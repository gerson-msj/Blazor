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
}
