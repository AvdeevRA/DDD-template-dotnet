using System.Linq.Expressions;
using DDD.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDD.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, new()
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entity)
    {
        await _dbContext.AddRangeAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>[]? include = null,
        bool asNoTracking = true,
        int? skip = null,
        int? take = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    )
    {
        var query = asNoTracking
            ? _dbContext.Set<TEntity>().AsNoTracking().AsQueryable()
            : _dbContext.Set<TEntity>().AsQueryable();

        if (include != null)
        {
            foreach (var includeValue in include)
            {
                query = query.Include(includeValue);
            }
        }

        if (skip != null && skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take != null && take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<List<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>[]? include = null,
        bool asNoTracking = true,
        int? skip = null,
        int? take = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    )
    {
        var query = asNoTracking
            ? _dbContext.Set<TEntity>().AsNoTracking().AsQueryable()
            : _dbContext.Set<TEntity>().AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            foreach (var includeValue in include)
            {
                query = query.Include(includeValue);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip != null && skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take != null && take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _ = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entity)
    {
        _dbContext.UpdateRange(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        _dbContext.Remove(entity);
        return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<int> DeleteRangeAsync(List<TEntity> entity)
    {
        _dbContext.RemoveRange(entity);
        return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
