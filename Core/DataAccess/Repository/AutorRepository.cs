using System;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.DataAccess.Repository;

public class AutorRepository(DataContext dataContext) : BaseRepository<AutorEntity>(dataContext)
{
    
}
