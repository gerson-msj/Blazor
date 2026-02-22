using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Service;

public class AutorService(DataFactory data)
{
    public Task<List<AutorEntity>> ToListAsync() =>
        data.ExecuteAsync(uow => uow.AutorRepository.ToListAsync());
}
