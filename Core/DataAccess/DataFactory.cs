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

    public async Task<TResult> ExecuteAsync<TResult>(
        Func<Uow, Task<TResult>> operation,
        CancellationToken ct = default)
    {
        await using Uow uow = await CreateUowAsync(ct);
        return await operation(uow);
    }

    public async Task ExecuteAsync(
        Func<Uow, Task> operation,
        CancellationToken ct = default)
    {
        await using Uow uow = await CreateUowAsync(ct);
        await operation(uow);
    }
}
