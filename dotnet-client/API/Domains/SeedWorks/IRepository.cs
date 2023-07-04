// ReSharper disable UnusedType.Global
// ReSharper disable UnusedTypeParameter

using Domain.SeedWorks;

namespace API.Domains.SeedWorks;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
