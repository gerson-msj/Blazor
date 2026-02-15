using Blazor.Core.DataAccess.Repository;

namespace Blazor.Core.DataAccess;

public class Uow(
    DataContext dataContext,
    LivroRepository livroRepository) : IAsyncDisposable
{
    private readonly DataContext _dataContext = dataContext;
    private bool _disposed;

    public LivroRepository LivroRepository { get; } = livroRepository;

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return _dataContext.SaveChangesAsync(ct);
    }

    public async ValueTask DisposeAsync()
    {
        
        if (_disposed) return;
        _disposed = true;
        await _dataContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
