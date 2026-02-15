using Blazor.Core.Domain.Entity;

namespace Blazor.Core.DataAccess.Repository;

public class LivroRepository(DataContext dataContext) : BaseRepository<LivroEntity>(dataContext)
{
    
}
