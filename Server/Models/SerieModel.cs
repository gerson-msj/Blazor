namespace Blazor.Server.Models;

public class SerieModel
{
    public SelectItemModel Serie { get; set; } = new(0, string.Empty);
    public SelectItemModel Autor { get; set; } = new(0, string.Empty);
    public List<BaseLivroModel> Livros { get; set; } = [];
}
