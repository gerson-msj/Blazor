using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class SerieDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;

    public SerieDto() { }

    public SerieDto(SerieEntity entity)
    {
        Id = entity.Id;
        Nome = entity.Nome;
    }
}