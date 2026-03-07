using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class SerieDto : ListaDto
{
    public AutorDto Autor { get; set; } = default!;
    public List<LivroDto> Livros { get; set; } = [];

    public SerieDto() { }

    public SerieDto(SerieEntity entity)
    {
        Id = entity.Id;
        Nome = entity.Nome;
        Autor = new(entity.Autor);
        Livros = [.. entity.Livros.Select(l => new LivroDto(l))];
    }

    public SerieEntity ToNewEntity() => new()
    {
        Id = Id,
        Nome = Nome,
        Autor = Autor.ToNewEntity(),
        Livros = [.. Livros.Select(livroDto => livroDto.ToNewEntity())]
    };
}