using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Core.DataAccess.Repository;

public abstract class BaseRepository<TEntity>(DataContext dataContext) where TEntity : BaseEntity
{
    protected DbSet<TEntity> DbSet => dataContext.Set<TEntity>();
}
