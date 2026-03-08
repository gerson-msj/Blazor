using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class SerieDto : ListaDto, IValidatableObject
{
    public AutorDto Autor { get; set; } = default!;
    public List<LivroDto> Livros { get; set; } = [];

    public SerieDto() { }

    public SerieDto(SerieEntity entity, bool incluirLivros = false)
    {
        Id = entity.Id;
        Nome = entity.Nome;
        Autor = new(entity.Autor);
        Livros = [.. entity.Livros.Select(l => new LivroDto(l, false))];
    }

    public SerieEntity ToNewEntity() => new()
    {
        Id = Id,
        Nome = Nome,
        Autor = Autor.ToNewEntity(),
        Livros = [.. Livros.Select(livroDto => livroDto.ToNewEntity())]
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Nome))
            yield return new("Informe o nome da série.", [nameof(Nome)]);
    }
}