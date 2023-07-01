using RabbitMQ.Client;

namespace API.Applications.Services;

public class RabbitMqClientFactory
{
    private readonly string _hostName;

    public RabbitMqClientFactory(string hostName)
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
