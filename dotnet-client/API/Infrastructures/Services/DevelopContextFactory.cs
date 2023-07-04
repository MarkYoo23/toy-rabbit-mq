using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Infrastructures.Services;

public class DevelopContextFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public DevelopContextFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public T Create<T>(string connectionString) where T : DbContext
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
        var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString)
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .ConfigureWarnings(configurationBuilder => configurationBuilder.Log(
                (RelationalEventId.TransactionStarted, LogLevel.Information),
                (RelationalEventId.TransactionCommitted, LogLevel.Information),
                (RelationalEventId.TransactionRolledBack, LogLevel.Information)))
            .UseLoggerFactory(_loggerFactory);

        return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
    }
}
