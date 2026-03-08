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

    public async Task<List<SerieDto>> Listar()
    {
        await using Uow uow = await data.CreateUowAsync();
        var entities = await uow.SerieRepository.Listar();
        List<SerieDto> dtos = [.. entities.Select(e => new SerieDto(e, true))];
        return dtos;
    }

    public async Task<SerieDto?> Obter(int id)
    {
        await using Uow uow = await data.CreateUowAsync();
        var entity = await uow.SerieRepository.Obter(id);
        return entity is null ? default : new(entity, true);
    }
}
