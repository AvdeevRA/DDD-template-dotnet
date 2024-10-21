using System.Linq.Expressions;

namespace DDD.DataAccess.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : class, new()
{
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<List<TEntity>> AddRangeAsync(List<TEntity> entity);
    public Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>[]? include = null,
        bool asNoTracking = true,
        int? skip = null,
        int? take = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    );
    public Task<List<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>[]? include = null,
        bool asNoTracking = true,
        int? skip = null,
        int? take = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    );
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entity);
    public Task<int> DeleteAsync(TEntity entity);
    public Task<int> DeleteRangeAsync(List<TEntity> entity);
}
