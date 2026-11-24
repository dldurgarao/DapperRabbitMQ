using System;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DapperRabbitMQ.MessageBus;

public class Publisher
{
    private readonly IConnection _connection;
    public Publisher(IConnection connection)
    {
        _connection = connection;
    }
    public async Task PublishMessageAsync<T>(string queue, T message)
    {
        // Channels are not thread-safe, so use one per operation
        await using var channel = await _connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        var props = new BasicProperties { Persistent = true };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queue,
            mandatory: false,
            basicProperties: props,
            body: body);
    }
    
}
