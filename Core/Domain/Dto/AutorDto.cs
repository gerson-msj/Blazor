using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class AutorDto : ListaDto
{
    public AutorDto() { }

    public AutorDto(AutorEntity entity)
    {
        Id = entity.Id;
        Nome = entity.Nome;
    }

    public void ApplyToEntity(AutorEntity entity)
    {
        entity.Id = Id;
        entity.Nome = Nome;
    }

    public AutorEntity ToNewEntity()
    {
        AutorEntity entity = new();
        ApplyToEntity(entity);
        return entity;
    }
}
