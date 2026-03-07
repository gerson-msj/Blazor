using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Dto;

namespace Blazor.Server.Models;

public class SerieModel : IValidatableObject
{
    public SelectItemModel? Serie { get; set; }
    public SelectItemModel? Autor { get; set; }
    public List<BaseLivroModel> Livros { get; set; } = [];

    public SerieModel() { }

    public SerieModel(SerieDto dto)
    {
        Serie = new(dto.Id, dto.Nome);
        Autor = new(dto.Autor.Id, dto.Autor.Nome);
        Livros = [.. dto.Livros.Select(livroDto => new BaseLivroModel(livroDto))];
    }

    public SerieDto ToDto() => new()
    {
        Id = Serie?.Id ?? 0,
        Nome = Serie?.Name ?? string.Empty,
        Autor = new()
        {
            Id = Autor?.Id ?? 0,
            Nome = Autor?.Name ?? string.Empty
        },
        Livros = [.. Livros.Select(livro => livro.ToDto())]
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {


        if (Serie is null)
            yield return new("Informe o nome da série.", [nameof(Serie)]);

        if (Autor is null)
            yield return new("Informe o autor da série.", [nameof(Autor)]);

        int qtdLivrosAtivos = Livros.Count(l => !l.Excluido);
        if (qtdLivrosAtivos < 2)
        {
            yield return new("Uma série deve conter ao menos 2 livros.", [nameof(Livros)]);
            yield break;
        }

        List<Tuple<int, BaseLivroModel>> livrosIndexados = [];

        if (Livros.Any(l => string.IsNullOrWhiteSpace(l.Titulo) && !l.Ordem.HasValue && !l.Excluido))
        {
            yield return new("Existe um ou mais livros sem título e ordenação.", [nameof(Livros)]);
            yield break;
        }

        bool stopValidation = false;
        for (int i = 0; i < Livros.Count; i++)
        {
            var livro = Livros[i];

            if (livro.Excluido)
                continue;

            livrosIndexados.Add(Tuple.Create(i, livro));

            var possuiTitulo = !string.IsNullOrWhiteSpace(livro.Titulo);
            var possuiOrdem = livro.Ordem.HasValue;
            var ordemInvalida = possuiOrdem && (livro.Ordem < 1 || livro.Ordem > 99);
            var memberNameTitulo = $"{nameof(Livros)};{i};{nameof(BaseLivroModel.Titulo)}";
            var memberNameOrdem = $"{nameof(Livros)};{i};{nameof(BaseLivroModel.Ordem)}";

            if (!possuiTitulo && possuiOrdem)
            {
                yield return new($"O título do livro {livro.Ordem} não foi informado.", [memberNameTitulo]);
                stopValidation = true;
            }

            if (!possuiOrdem && possuiTitulo)
            {
                yield return new($"A ordem do livro {livro.Titulo} não foi informada.", [memberNameOrdem]);
                stopValidation = true;
            }

            if (possuiTitulo && ordemInvalida)
            {
                yield return new($"A ordem do livro {livro.Titulo} é inválida.", [memberNameOrdem]);
                stopValidation = true;
            }
        }


        if (!stopValidation)
        {
            var livrosOrdenados = livrosIndexados.OrderBy(i => i.Item2.Ordem).ToList();
            for (int i = 0; i < livrosOrdenados.Count; i++)
            {
                var indice = livrosOrdenados[i].Item1;
                var livro = livrosOrdenados[i].Item2;

                if (livro.Ordem != i + 1)
                {
                    var memberNameOrdem = $"{nameof(Livros)};{indice};{nameof(BaseLivroModel.Ordem)}";
                    yield return new($"A ordem {livro.Ordem} do livro {livro.Titulo} é inválida, deveria ser {i + 1}.", [memberNameOrdem]);
                }
            }
        }
    }
}
