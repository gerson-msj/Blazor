using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Dto;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Service;

public class SerieService(DataFactory data)
{
    public Task Create(SerieDto dto) => Create(dto.ToNewEntity());

    public async Task Create(SerieEntity entity)
    {
        await using Uow uow = await data.CreateUowAsync();
        uow.SerieRepository.Add(entity);
        await uow.SaveChangesAsync();
    }
}
