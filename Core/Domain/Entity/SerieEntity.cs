namespace Blazor.Core.Domain.Entity;

public class SerieEntity : BaseEntity
{
    public string Nome { get; set; } = null!;
    public AutorEntity Autor { get; set; } = default!;
    public ICollection<LivroEntity> Livros { get; set; } = [];
}
