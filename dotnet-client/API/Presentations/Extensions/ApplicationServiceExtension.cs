using API.Applications.Services;

namespace API.Presentations.Extensions;

public static class ApplicationServiceExtension
{
    public static void AddApplicationService(this IServiceCollection service)
    {
        service.AddSingleton<RabbitMqClientFactory>(serviceProvider =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var hostUrl = config.GetConnectionString("RabbitMq")!;
            return new RabbitMqClientFactory(hostUrl);
        });

        service.AddScoped<SayHelloService>();
    }
}
