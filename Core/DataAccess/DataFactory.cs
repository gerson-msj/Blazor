using Microsoft.EntityFrameworkCore;

namespace Blazor.Core.DataAccess;

public sealed class DataFactory(IDbContextFactory<DataContext> contextFactory)
{
    public async Task<Uow> CreateUowAsync(CancellationToken ct = default)
    {
        var dataContext = await contextFactory.CreateDbContextAsync(ct);
        return new(
            dataContext,
            livroRepository: new(dataContext)
        );
    }
}
