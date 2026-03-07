using System;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.DataAccess.Repository;

public class SerieRepository(DataContext dataContext) : BaseRepository<SerieEntity>(dataContext)
{

}
