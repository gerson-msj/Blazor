using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class LivroDto : IValidatableObject
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public AutorDto? Autor { get; set; }
    public SerieDto? Serie { get; set; }
    public int? Ordem { get; set; }
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }

    public bool Excluido { get; set; }
    public Guid Key { get; private set; } = Guid.NewGuid();

    public LivroDto() { }

    public LivroDto(LivroEntity entity, bool incluirSerie = false)
    {
        Id = entity.Id;
        Titulo = entity.Titulo;
        Autor = entity.Autor is not null ? new(entity.Autor) : null;

        if (entity.Serie is not null && incluirSerie)
            Serie = new(entity.Serie);

        Ordem = entity.Ordem;
        DataConclusao = entity.DataConclusao;
        Comentarios = entity.Comentarios;
    }

    public void ApplyToEntity(LivroEntity entity, bool applyId = false)
    {
        if (applyId) entity.Id = entity.Id;

        entity.Titulo = Titulo;

        if (Autor is null)
        {
            entity.Autor = default;
        }
        else
        {
            entity.Autor ??= new();
            Autor.ApplyToEntity(entity.Autor);
        }

        if (Serie is null)
        {
            entity.Serie = default;
        }
        else
        {
            entity.Serie ??= new SerieEntity();
            entity.Serie.Id = Serie.Id;
            entity.Serie.Nome = Serie.Nome;
        }

        entity.Ordem = Ordem;
        entity.DataConclusao = DataConclusao;
        entity.Comentarios = Comentarios;
    }

    public LivroEntity ToNewEntity()
    {
        var entity = new LivroEntity();
        ApplyToEntity(entity);
        return entity;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Excluido)
            yield break;

        var possuiTitulo = !string.IsNullOrWhiteSpace(Titulo);
        var possuiOrdem = Ordem.HasValue;
        var ordemInvalida = possuiOrdem && Ordem < 1 || Ordem > 99;
        if (!possuiTitulo && possuiOrdem)
            yield return new($"O título do livro {Ordem} não foi informado.", [nameof(Titulo)]);

        if (!possuiOrdem && possuiTitulo)
            yield return new($"A ordem do livro {Ordem} não foi informada.", [nameof(Ordem)]);

        if (possuiTitulo && ordemInvalida)
            yield return new($"A ordem do livro {Ordem} é inválida.", [nameof(Ordem)]);
    }
}
