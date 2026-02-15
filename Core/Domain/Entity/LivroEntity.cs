namespace Blazor.Core.Domain.Entity;

public class LivroEntity : BaseEntity
{
    public string Titulo { get; set; } = "";
    public int IdAutor { get; set; }

    public virtual AutorEntity Autor { get; set; } = null!;
}
