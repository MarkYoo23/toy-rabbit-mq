using System.Linq.Expressions;

namespace Domain.SeedWorks;

public interface IQueryRepository<T> where T : ValueObject
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> FindAll(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression);
}
