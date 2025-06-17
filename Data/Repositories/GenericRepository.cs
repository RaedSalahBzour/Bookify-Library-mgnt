using Data.Helpers;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class GenericRepository<T>(ApplicationDbContext context)
    : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();


    public Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
            query = include(query);

        return query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }


    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task<T> Delete(T entity)
    {
        _dbSet.Remove(entity);
        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
