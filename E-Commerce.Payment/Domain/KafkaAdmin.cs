using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace E_Commerce.Payment.Domain;

public static class KafkaAdmin
{
    public static async Task CreateTopicAsync(string bootstrapServers, string topicName)
    {
        Console.WriteLine($"Creation of {topicName}");
        using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();
        try
        {
            await adminClient.CreateTopicsAsync(new [] { 
                new TopicSpecification { Name = topicName, ReplicationFactor = 1, NumPartitions = 1 } });
        }
        catch (CreateTopicsException e)
        {
            Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
        }
    }
}