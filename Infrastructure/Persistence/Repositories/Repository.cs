
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
    public void Add(TEntity entity)
    {
        _placeDbContext.Set<TEntity>().Add(entity);
        _placeDbContext.SaveChanges();
    }
    public void AddMany(IEnumerable<TEntity> entities)
    {
        _placeDbContext.Set<TEntity>().AddRange(entities);
        _placeDbContext.SaveChanges();
    }
    public void Delete(TEntity entity)
    {
        _placeDbContext.Set<TEntity>().Remove(entity);
        _placeDbContext.SaveChanges();
    }
    public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = Find(predicate);
        _placeDbContext.Set<TEntity>().RemoveRange(entities);
        _placeDbContext.SaveChanges();
    }
    public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).FirstOrDefault(predicate)!;
    }
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).Where(predicate);
    }
    public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
    {
        return Get(findOptions);
    }
    public void Update(TEntity entity)
    {
        _placeDbContext.Set<TEntity>().Update(entity);
        _placeDbContext.SaveChanges();
    }
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _placeDbContext.Set<TEntity>().Any(predicate);
    }
    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _placeDbContext.Set<TEntity>().Count(predicate);
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
