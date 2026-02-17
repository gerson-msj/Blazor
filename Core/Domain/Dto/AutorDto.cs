using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class AutorDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;

    public AutorDto() { }

    public AutorDto(AutorEntity entity)
    {
        Id = entity.Id;
        Nome = entity.Nome;
    }
}
