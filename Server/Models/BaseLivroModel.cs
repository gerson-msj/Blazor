using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Server.Models;

public class BaseLivroModel
{
    public Guid Key { get; set; } = Guid.NewGuid();
    public bool Excluido { get; set; }
    public int Id { get; set; }

    [DisplayName("Título")]
    [Required(ErrorMessage = "Informe o {0}.")]
    public string Titulo { get; set; } = string.Empty;
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }
    public int? Ordem { get; set; }
}
