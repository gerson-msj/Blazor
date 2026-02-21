using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class LivroDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public AutorDto Autor { get; set; } = null!;
    public SerieDto? Serie { get; set; }
    public int? Ordem { get; set; }
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }

    public LivroDto() { }

    public LivroDto(LivroEntity entity)
    {
        Id = entity.Id;
        Titulo = entity.Titulo;
        Autor = new(entity.Autor);

        if (entity.Serie is not null)
            Serie = new(entity.Serie);

        Ordem = entity.Ordem;
        DataConclusao = entity.DataConclusao;
        Comentarios = entity.Comentarios;
    }

    public void ApplyToEntity(LivroEntity entity)
    {
        entity.Titulo = Titulo;

        entity.Autor ??= new AutorEntity();
        entity.Autor.Id = Autor.Id;
        entity.Autor.Nome = Autor.Nome;

        if (Serie is null)
        {
            entity.Serie = null;
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
}
