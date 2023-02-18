using System.Linq.Expressions;
using E_Commerce.API.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Repositories;
public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly EFDBContext _context;

    protected BaseRepository(EFDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity item)
    {
        var response = await _dbSet.AddAsync(item);
        return response.Entity;
    }

    public async Task<TEntity> FindAsync(params object[] ids)
    {
        return await _dbSet.FindAsync(ids);
    }

    public TEntity Update(TEntity item)
    {
        return _dbSet.Update(item).Entity;
    }
    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take)
    {
        return await _dbSet.AsNoTracking().Where(predicate).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<bool> RemoveAsync(TEntity item)
    {
        var removedSuccessfully = _dbSet.Remove(item) != null;
        return await Task.FromResult(removedSuccessfully);
    }

        
}