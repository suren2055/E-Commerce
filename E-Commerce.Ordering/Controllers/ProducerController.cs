using System.Net;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Ordering.Controllers;
[ApiController]
[Route("[controller]")]
public class ProducerController : ControllerBase
{
    private const string bootstrapServers = "broker:29092";
    private const string topic = "ordering-topic";

    [HttpGet]
    public IActionResult Get()
    {
        SendOrderRequest(topic, $"Test from {topic}");
        return Ok();
    }

    private static async Task SendOrderRequest(string topic, string message)
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