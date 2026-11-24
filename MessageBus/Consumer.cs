using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Text.Json;
using DapperRabbitMQ.Database;
using Microsoft.Extensions.Caching.Distributed;

namespace DapperRabbitMQ.MessageBus;

public class Consumer : BackgroundService
{
    private const string QueueName = "department-created";
    private readonly IConnection _connection;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<Consumer> _logger;
    private IChannel _channel;
    private readonly IDistributedCache _cache;

    public Consumer(IConnection connection, IServiceScopeFactory serviceScopeFactory, ILogger<Consumer> logger, IDistributedCache cache)
    {
        _connection = connection;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _cache = cache;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.QueueDeclareAsync(QueueName, durable: true,
            exclusive: false, autoDelete: false, arguments: null,
            cancellationToken: stoppingToken);

        // Process one message at a time
        await _channel.BasicQosAsync(0, 1, false, stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var dept = JsonSerializer.Deserialize<Models.Department>(json);
                _logger.LogInformation("Received department: {Name}", dept?.ToString());

                // Scoped services (DbContext, etc.) must be resolved inside a scope
                using var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                // await db.SomethingAsync(...);
                

                Console.WriteLine($"Received department: {dept?.ToString()}");

                await _channel!.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Message processing failed");
                // requeue: false avoids an infinite retry loop on bad messages
                await _channel!.BasicNackAsync(ea.DeliveryTag, false, requeue: false);
            }
        };

        await _channel.BasicConsumeAsync(QueueName, autoAck: false,
            consumer: consumer, cancellationToken: stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) await _channel.CloseAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }

}
