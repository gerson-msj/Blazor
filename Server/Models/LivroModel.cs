namespace Blazor.Server.Models;

public class LivroModel : BaseLivroModel
{
    public SelectItemModel Autor { get; set; } = new(0, string.Empty);
}
