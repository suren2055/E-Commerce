using System.Net;
using Confluent.Kafka;
using E_Commerce.Ordering.Entities;
using E_Commerce.Ordering.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Ordering.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderingController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderingController> _logger;
    private const string bootstrapServers = "broker:29092";
    private const string topic = "ordering-topic";

    public OrderingController(IOrderRepository orderRepository, ILogger<OrderingController> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    [HttpPost("PlaceOrder")]
    public IActionResult PlaceOrder(Order order)
    {
        try
        {
            var o = _orderRepository.CreateAsync(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Produce(topic, JsonConvert.SerializeObject(order));
        }


        return Ok(order.OrderId);
    }

    private static async Task Produce(string topic, string message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            ClientId = Dns.GetHostName()
        };

        try
        {
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var result = await producer.ProduceAsync
            (topic, new Message<Null, string>
            {
                Value = message
            });

            Console.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
            Console.WriteLine($"Delivery Message:{message}");
            await Task.FromResult(true);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }

        await Task.FromResult(false);
    }
}