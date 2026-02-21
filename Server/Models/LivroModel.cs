using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Dto;

namespace Blazor.Server.Models;

public class LivroModel : BaseLivroModel
{
    [Required(ErrorMessage = "Informe o {0}.")]
    public SelectItemModel? Autor { get; set; }

    public LivroDto ToDto() => new()
    {
        Id = Id,
        Titulo = Titulo,
        Autor = new AutorDto()
        {
            Id = Autor?.Id ?? 0,
            Nome = Autor?.Name ?? string.Empty
        },
        DataConclusao = DataConclusao,
        Comentarios = Comentarios
    };

}
