using System.Reflection;
using API.Applications.Services;
using API.Applications.Services.RabbitMqs;
using API.Domains.Users;
using API.Infrastructures.Contexts;
using API.Infrastructures.Repositories;
using API.Infrastructures.Services;

namespace API.Presentations.Extensions;

public static class ApplicationServiceExtension
{
    public static void AddApplicationService(this IServiceCollection services)
    {
        // MediatR
        // install Microsoft.Extensions.DependencyInjection.Abstractions
        // install MediatR
        // install MediatR.Extensions.Microsoft.DependencyInjection
        // install MediatR.Contracts
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        // Sql Servers
        services.AddSingleton<DevelopContextFactory>();
        services.AddApplicationContext<UserContext>("UserSqlServer");

        services.AddScoped<IUserRepository, UserRepository>();

        // Rabbit MQs
        services.AddSingleton<RabbitMqConnectionFactory>(serviceProvider =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var hostUrl = config.GetConnectionString("RabbitMq")!;
            return new RabbitMqConnectionFactory(hostUrl);
        });
        services.AddTransient<HelloChannelFactory>();

        services.AddScoped<SayHelloService>();
        services.AddSingleton<ReceiveHelloService>();
    }
}
