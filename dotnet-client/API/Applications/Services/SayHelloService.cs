using System.Text;
using API.Applications.Services.RabbitMqs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API.Applications.Services;

public class SayHelloService
{
    private readonly ILogger<SayHelloService> _logger;
    private readonly HelloChannelFactory _helloChannelFactory;

    public SayHelloService(
        ILogger<SayHelloService> logger,
        HelloChannelFactory helloChannelFactory)
    {
        _logger = logger;
        _helloChannelFactory = helloChannelFactory;
    }

    public void Execute()
    {
        var channel = _helloChannelFactory.Channel;

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
    }
}
