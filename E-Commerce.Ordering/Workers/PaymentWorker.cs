using Confluent.Kafka;
using E_Commerce.Ordering.Domain;
using E_Commerce.Ordering.Models;
using E_Commerce.Ordering.Repositories;
using Newtonsoft.Json;

namespace E_Commerce.Ordering.Workers;

public class PaymentWorker : BackgroundService
{
    private const string topic = "payment-topic";
    private const string groupId = "payment-service";
    private const string bootstrapServers = "broker:29092";
    private readonly IOrderRepository _orderRepository;


    public PaymentWorker(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    private void RunConsuming(IConsumer<Ignore, string> consumerBuilder,
        CancellationTokenSource cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var consume = consumerBuilder.Consume(cancellationToken.Token);
            var json = consume.Message.Value;
            Console.WriteLine("Json: " + consume.Message.Value);
            var payment = JsonConvert.DeserializeObject<Payment>(json);
            if (payment.TransactionStatus == 1)
            {
                try
                {
                    var order = _orderRepository.FindAsync(5).Result;
                    order.PaymentStatus = 1;
                    _orderRepository.Update(order);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using (var adminClient =
               new AdminClientBuilder(new AdminClientConfig {BootstrapServers = bootstrapServers}).Build())
        {
            var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
            if (meta.Topics.All(x => x.Topic != topic))
                await KafkaAdmin.CreateTopicAsync(bootstrapServers, topic);
        }

        try
        {
            using var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build();
            consumerBuilder.Subscribe(topic);
            var cancelToken = new CancellationTokenSource();

            try
            {
                await Task.Run(() => RunConsuming(consumerBuilder, cancelToken), cancelToken.Token);
            }
            catch (OperationCanceledException)
            {
                consumerBuilder.Close();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        await Task.CompletedTask;
    }
}