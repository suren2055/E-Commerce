using System.Linq.Expressions;

namespace E_Commerce.Ordering.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<TEntity> CreateAsync(TEntity item);

    public Task<TEntity> FindAsync(params object[] ids);

    public TEntity Update(TEntity item);
    public Task<bool> RemoveAsync(TEntity item);

    public Task<IEnumerable<TEntity>> GetAsync();

    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take);

    public Task Complete();
}