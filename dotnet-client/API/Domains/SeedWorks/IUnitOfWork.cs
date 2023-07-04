namespace Domain.SeedWorks;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default, int? expectRows = null);

    Task TransactionStartAsync();
    Task TransactionCommitAsync();
    Task TransactionRollbackAsync();
    Task TransactionEndAsync();

    IEnumerable<string> GetTransactionSavePoints(string name);
    Task TransactionCreateSavePointAsync(string name);
    Task TransactionRollbackSavePointAsync(string name);
}
