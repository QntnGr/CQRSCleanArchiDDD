
using Application.Common.Interfaces.Persistance;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly PlaceDbContext _placeDbContext;
    public Repository(PlaceDbContext placeDbContext)
    {
        _placeDbContext = placeDbContext;
    }
    public async Task AddAsync(TEntity entity)
    {
        _placeDbContext.Set<TEntity>().Add(entity);
        await _placeDbContext.SaveChangesAsync();
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entities)
    {
        await _placeDbContext.Set<TEntity>().AddRangeAsync(entities);
        await _placeDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _placeDbContext.Set<TEntity>().Remove(entity);
        await _placeDbContext.SaveChangesAsync();
    }
    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return await Get(findOptions).FirstOrDefaultAsync(predicate);
    }
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).Where(predicate);
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(FindOptions? findOptions = null)
    {
        return await Get(findOptions).ToArrayAsync();
    }
    public async Task UpdateAsync(TEntity entity)
    {
        _placeDbContext.Set<TEntity>().Update(entity);
        await _placeDbContext.SaveChangesAsync();
    }
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _placeDbContext.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _placeDbContext.Set<TEntity>().CountAsync(predicate);
    }
    private DbSet<TEntity> Get(FindOptions? findOptions = null)
    {
        findOptions ??= new FindOptions();
        var entity = _placeDbContext.Set<TEntity>();
        if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
        {
            entity.IgnoreAutoIncludes().AsNoTracking();
        }
        else if (findOptions.IsIgnoreAutoIncludes)
        {
            entity.IgnoreAutoIncludes();
        }
        else if (findOptions.IsAsNoTracking)
        {
            entity.AsNoTracking();
        }
        return entity;
    }

}
