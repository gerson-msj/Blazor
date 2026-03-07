namespace Blazor.Core.Domain.Entity;

public class LivroEntity : BaseEntity
{
    public string Titulo { get; set; } = default!;
    public AutorEntity? Autor { get; set; }
    public SerieEntity? Serie { get; set; }
    public int? Ordem { get; set; }
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }
}
