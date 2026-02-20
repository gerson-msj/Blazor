namespace Blazor.Server.Models;

public class BaseLivroModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }
    public int? Ordem { get; set; }
}
