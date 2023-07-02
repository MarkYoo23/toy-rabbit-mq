using RabbitMQ.Client;

namespace API.Applications.Services.RabbitMqs;

public class HelloChannelFactory : IDisposable
{
    public HelloChannelFactory(RabbitMqConnectionFactory connectionFactory)
    {
        Connection = connectionFactory.Create();
        Channel = Connection.CreateModel();

        Channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public IConnection Connection { get;}
    public IModel Channel { get; }

    public void Dispose()
    {
        Channel.Dispose();
        Connection.Dispose();
    }
}
