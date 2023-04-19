using System.Text.Json.Serialization;
using Confluent.Kafka;
using E_Commerce.Payment.Domain;
using E_Commerce.Payment.Models;
using E_Commerce.Payment.Repositories;
using Newtonsoft.Json;

namespace E_Commerce.Payment.Workers;

public class OrderingWorker : BackgroundService
{
    private const string topic = "ordering-topic";
    private const string groupId = "payment-service";
    private const string bootstrapServers = "broker:29092";
    private readonly IPaymentRepository _paymentRepository;

    public OrderingWorker(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    private async Task RunConsuming(IConsumer<Ignore, string> consumerBuilder,
        CancellationTokenSource cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var consume = consumerBuilder.Consume(cancellationToken.Token);
            var json = consume.Message.Value;
            Console.WriteLine("Json: " + consume.Message.Value);
            var order = JsonConvert.DeserializeObject<Order>(json);
            if (order.Payment != null)
            {
                try
                {
                    //Makes payment when already placed order has payment
                    var p = await _paymentRepository.CreateAsync(order.Payment);
                    Console.WriteLine("Placed payment: " + p.PaymentID);
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