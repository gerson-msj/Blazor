using System;
using Blazor.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Core.DataAccess.Repository;

public class SerieRepository(DataContext dataContext) : BaseRepository<SerieEntity>(dataContext)
{
    public Task<List<SerieEntity>> Listar() =>
        DbSet
            .Include(e => e.Autor)
            .Include(e => e.Livros)
            .ToListAsync();

    public Task<SerieEntity?> Obter(int id) =>
        DbSet
            .Include(e => e.Autor)
            .Include(e => e.Livros)
            .FirstOrDefaultAsync(e => e.Id == id);
}
