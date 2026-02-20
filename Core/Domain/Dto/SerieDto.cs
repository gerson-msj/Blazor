using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class SerieDto : ListaDto
{
    public SerieDto() { }

    public SerieDto(SerieEntity entity)
    {
        Id = entity.Id;
        Nome = entity.Nome;
    }
}