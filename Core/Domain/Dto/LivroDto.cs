using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Domain.Dto;

public class LivroDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;

    public int IdAutor { get; set; }
    public string NomeAutor { get; set; } = string.Empty;

    public int? IdSerie { get; set; }
    public string? NomeSerie { get; set; }

    public int? Ordem { get; set; }
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }

    public LivroDto() { }

    public LivroDto(LivroEntity entity)
    {
        Id = entity.Id;
        Titulo = entity.Titulo;
        IdAutor = entity.Autor.Id;
        NomeAutor = entity.Autor.Nome;
        IdSerie = entity.Serie?.Id;
        NomeSerie = entity.Serie?.Nome;
        Ordem = entity.Ordem;
        DataConclusao = entity.DataConclusao;
        Comentarios = entity.Comentarios;
    }

    public void ApplyToEntity(LivroEntity entity)
    {
        entity.Titulo = Titulo;

        entity.Autor ??= new AutorEntity();
        entity.Autor.Id = IdAutor;
        entity.Autor.Nome = NomeAutor;

        if (IdSerie is null)
        {
            entity.Serie = null;
        }
        else
        {
            entity.Serie ??= new SerieEntity();
            entity.Serie.Id = IdSerie.Value;
            entity.Serie.Nome = NomeSerie ?? string.Empty;
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
