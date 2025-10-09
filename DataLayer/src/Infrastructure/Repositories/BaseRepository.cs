using System.Linq.Expressions;
using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository where T : class
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext appDbContext)
    {
        _context = appDbContext;
    }
     public virtual T Insert(T entity)
    {
        return _context
            .Add(entity)
            .Entity;    
    }

    public virtual T Update(T entity)
    {
        return _context.Update(entity)
            .Entity;    
    }

    public virtual bool Delete(T entity)
    {
        _context.Remove(entity);
        
        var count = _context.SaveChanges();
        return count >= 1;
    }

    public async virtual Task<T?> Get(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.FindAsync<T>(new object[] {id}, cancellationToken);
        }
        catch (ArgumentException)
        {
            // The entity type doesn't support int keys, return null
            return null;
        }
    }

    public async virtual Task<T?> Get(string id, CancellationToken cancellationToken)
    {
        // Try to parse as Guid first, then fall back to string
        if (Guid.TryParse(id, out var guidId))
        {
            return await _context.FindAsync<T>(new object[] {guidId}, cancellationToken);
        }
        return await _context.FindAsync<T>(new object[] {id}, cancellationToken);
    }

    public virtual async Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.FindAsync<T>(new object[] {id}, cancellationToken);
    }

    public virtual async Task<T?> Get(object id, CancellationToken cancellationToken)
    {
        return await _context.FindAsync<T>(new object[] {id}, cancellationToken);
    }
    
    public virtual async Task<IEnumerable<T?>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool noTrack = false)
    {
        var query = _context.Set<T>()
            .AsQueryable()
            .Where(predicate);
        
        if(noTrack)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync(cancellationToken);    
    }

    public async Task<IEnumerable<T?>> All(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var result = await _context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .CountAsync(cancellationToken);

        return result > 0;
    }

    public async virtual Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChanges(cancellationToken);

    }
}