using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API.Applications.Services;

public class SayHelloService
{
    private readonly ILogger<SayHelloService> _logger;
    private readonly RabbitMqClientFactory _rabbitMqClientFactory;

    public SayHelloService(
        ILogger<SayHelloService> logger,
        RabbitMqClientFactory rabbitMqClientFactory)
    {
        _logger = logger;
        _rabbitMqClientFactory = rabbitMqClientFactory;
    }

    public async Task ExecuteAsync()
    {
        using var connection = _rabbitMqClientFactory.Create();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        const string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: body);

        _logger.LogInformation($"Rabbit MQ : Publish : {message}");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation($"Rabbit MQ : Received : {message}");
        };

        channel.BasicConsume(queue: "hello",
            autoAck: true,
            consumer: consumer);

        await Task.Delay(5000);
    }
}
