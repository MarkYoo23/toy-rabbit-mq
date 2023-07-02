using RabbitMQ.Client;

namespace API.Applications.Services.RabbitMqs;

public class RabbitMqConnectionFactory
{
    private readonly string _hostName;

    public RabbitMqConnectionFactory(string hostName)
    {
        _hostName = hostName;
    }

    public IConnection Create()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _hostName,
            UserName = "user",
            Password = "qwer1234",
            VirtualHost = "rabbitmq_vhost"
        };

        return connectionFactory.CreateConnection();
    }
}
