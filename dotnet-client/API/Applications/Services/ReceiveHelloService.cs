using System.Text;
using API.Applications.Services.RabbitMqs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API.Applications.Services;

public class ReceiveHelloService
{
    private readonly ILogger<ReceiveHelloService> _logger;
    private readonly HelloChannelFactory _helloChannelFactory;

    public ReceiveHelloService(
        ILogger<ReceiveHelloService> logger,
        HelloChannelFactory helloChannelFactory)
    {
        _logger = logger;
        _helloChannelFactory = helloChannelFactory;
    }

    public void Register()
    {
        var channel = _helloChannelFactory.Channel;

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
    }
}
