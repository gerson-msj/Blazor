namespace Blazor.Core.Domain.Entity;

public class LivroEntity : BaseEntity
{
    public string Titulo { get; set; } = null!;
    public AutorEntity Autor { get; set; } = null!;
    public SerieEntity? Serie { get; set; }
    public int? Ordem { get; set; }
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }
}
