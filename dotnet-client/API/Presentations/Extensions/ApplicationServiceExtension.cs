using API.Applications.Services;
using API.Applications.Services.RabbitMqs;

namespace API.Presentations.Extensions;

public static class ApplicationServiceExtension
{
    public static void AddApplicationService(this IServiceCollection service)
    {
        service.AddSingleton<RabbitMqConnectionFactory>(serviceProvider =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var hostUrl = config.GetConnectionString("RabbitMq")!;
            return new RabbitMqConnectionFactory(hostUrl);
        });
        service.AddTransient<HelloChannelFactory>();

        service.AddScoped<SayHelloService>();
        service.AddSingleton<ReceiveHelloService>();
    }
}
