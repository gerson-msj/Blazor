using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Core.DataAccess.Repository;

public abstract class BaseRepository<TEntity>(DataContext dataContext) where TEntity : BaseEntity
{
    protected DbSet<TEntity> DbSet => dataContext.Set<TEntity>();

    public Task<List<TEntity>> ToListAsync() =>
        DbSet.ToListAsync();

    public ValueTask<TEntity?> FindAsync(int id) =>
        DbSet.FindAsync(id);

    public void Add(TEntity entity) =>
        DbSet.Add(entity);

    public void Remove(TEntity entity) =>
        DbSet.Remove(entity);
}
