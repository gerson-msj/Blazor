using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Dto;

namespace Blazor.Server.Models;

public class BaseLivroModel : IValidatableObject
{
    public Guid Key { get; set; } = Guid.NewGuid();
    public bool Excluido { get; set; }
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateOnly? DataConclusao { get; set; }
    public string? Comentarios { get; set; }
    public int? Ordem { get; set; }

    public BaseLivroModel() { }
    public BaseLivroModel(LivroDto livro)
    {
        Id = livro.Id;
        Titulo = livro.Titulo;
        DataConclusao = livro.DataConclusao;
        Comentarios = livro.Comentarios;
        Ordem = livro.Ordem;
        Excluido = livro.Excluido;
    }

    public LivroDto ToDto()
    {
        return new()
        {
            Id = Id,
            Titulo = Titulo,
            DataConclusao = DataConclusao,
            Comentarios = Comentarios,
            Ordem = Ordem,
            Excluido = Excluido
        };
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var valido = !Excluido && Ordem.HasValue && !string.IsNullOrWhiteSpace(Titulo);
        if (!valido)
            yield return new($"O título do livro {Ordem} não foi informado.", [$"{nameof(BaseLivroModel)}.{nameof(Titulo)}"]);
    }
}
