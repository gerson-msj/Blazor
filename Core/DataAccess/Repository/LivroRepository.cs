using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Core.DataAccess.Repository;

public class LivroRepository(DataContext dataContext) : BaseRepository<LivroEntity>(dataContext)
{
    public new Task<LivroEntity?> FindAsync(int id) =>
        DbSet
            .Include(e => e.Autor)
            .Include(e => e.Serie)
            .FirstOrDefaultAsync(e => e.Id == id);

    public new Task<List<LivroEntity>> ToListAsync() =>
        DbSet
            .Include(e => e.Autor)
            .Include(e => e.Serie)
            .ToListAsync();
}
