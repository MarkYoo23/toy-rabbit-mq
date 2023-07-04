using System.Transactions;
using Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Infrastructures.SeedWorks;

public abstract class BaseContext : DbContext, IUnitOfWork
{
    private readonly List<string> _savePoints = new();
    private IDbContextTransaction? _currentTransaction;

    protected BaseContext(DbContextOptions options) : base(options)
    {
    }

    protected abstract override void OnModelCreating(ModelBuilder modelBuilder);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var cts = CreateDefaultToken();

        if (cancellationToken == default)
        {
            cancellationToken = cts.Token;
        }

        return base.SaveChangesAsync(cancellationToken);
    }


    public async Task<bool> SaveEntitiesAsync(
        CancellationToken cancellationToken = default, int? expectRows = null)
    {
        using var cts = CreateDefaultToken();

        if (cancellationToken == default)
        {
            cancellationToken = cts.Token;
        }

        var actualRows = await SaveChangesAsync(cancellationToken);
        if (expectRows == null)
        {
            return actualRows > 0;
        }

        return actualRows == expectRows;
    }

    public async Task TransactionStartAsync()
    {
        if (IsTransactionInProgress())
        {
            throw new TransactionException("The transaction is currently running.");
        }

        _currentTransaction = await Database.BeginTransactionAsync();
    }

    public async Task TransactionCommitAsync()
    {
        if (!IsTransactionInProgress())
        {
            throw new TransactionException("The transaction is not currently running.");
        }

        await _currentTransaction!.CommitAsync();
    }

    public async Task TransactionRollbackAsync()
    {
        if (!IsTransactionInProgress())
        {
            throw new TransactionException("The transaction is not currently running.");
        }

        await _currentTransaction!.RollbackAsync();
    }

    public async Task TransactionEndAsync()
    {
        _savePoints.Clear();
        await _currentTransaction!.DisposeAsync();
        _currentTransaction = null!;
    }

    public IEnumerable<string> GetTransactionSavePoints(string name)
    {
        return _savePoints
            .Select(savePointName => new string(savePointName))
            .ToList();
    }

    public async Task TransactionCreateSavePointAsync(string name)
    {
        if (!IsTransactionInProgress())
        {
            throw new TransactionException("The transaction is not currently running.");
        }

        if (_savePoints.Contains(name))
        {
            throw new TransactionException("Save point cannot be duplicated.");
        }


        _savePoints.Add(name);
        await _currentTransaction!.CreateSavepointAsync(name);
    }

    public async Task TransactionRollbackSavePointAsync(string name)
    {
        if (!IsTransactionInProgress())
        {
            throw new TransactionException("The transaction is not currently running.");
        }

        if (!_savePoints.Contains(name))
        {
            throw new TransactionException("Save point cannot found.");
        }

        await _currentTransaction!.RollbackToSavepointAsync(name);
    }

    private bool IsTransactionInProgress() => _currentTransaction != null;
    private static CancellationTokenSource CreateDefaultToken() => new(5000);
}
