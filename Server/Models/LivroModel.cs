using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Dto;

namespace Blazor.Server.Models;

public class LivroModel : BaseLivroModel
{
    [Required(ErrorMessage = "Informe o {0}.")]
    public SelectItemModel? Autor { get; set; }

    public LivroModel() : base() { }

    public LivroModel(LivroDto dto) : base(dto)
    {
        Autor = dto.Autor is not null ? new(dto.Autor.Id, dto.Autor.Nome) : default;
    }

    public new LivroDto ToDto()
    {
        var dto = base.ToDto();
        if (Autor is not null)
        {
            dto.Autor = new() { Id = Autor.Id, Nome = Autor.Name };
        }

        return dto;
    }
}
