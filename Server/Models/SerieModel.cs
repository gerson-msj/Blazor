using System.ComponentModel.DataAnnotations;

namespace Blazor.Server.Models;

public class SerieModel : IValidatableObject
{
    public SelectItemModel? Serie { get; set; }
    public SelectItemModel? Autor { get; set; }
    public List<BaseLivroModel> Livros { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Serie is null)
            yield return new("Informe o nome da série.", [nameof(Serie)]);

        if (Autor is null)
            yield return new("Informe o autor da série.", [nameof(Autor)]);

        int qtdLivrosAtivos = Livros.Count(l => !l.Excluido);
        if (qtdLivrosAtivos < 2)
            yield return new("Uma série deve conter ao menos 2 livros.", [nameof(Livros)]);

        bool ordemCorreta = true;
        for (int i = 1; i <= qtdLivrosAtivos; i++)
        {
            int qtd = Livros.Count(l => l.Ordem == i && !l.Excluido);
            if (qtd != 1)
                ordemCorreta = false;
        }

        if (!ordemCorreta)
        {
            yield return new("A ordem de livros informada está incorreta.", [nameof(Livros)]);
        }
        else
        {
            for (int i = 0; i < Livros.Count; i++)
            {
                var l = Livros[i];
                var valido = !l.Excluido && l.Ordem.HasValue && !string.IsNullOrWhiteSpace(l.Titulo);
                if (!valido)
                {
                    var memberName = $"{nameof(Livros)};{i};{nameof(BaseLivroModel.Titulo)}";
                    yield return new($"O título do livro {l.Ordem} não foi informado.", [memberName]);
                }
            }
            // var livrosInvalidos = Livros.Where(l =>
            //     !l.Excluido
            //     && l.Ordem.HasValue
            //     && string.IsNullOrWhiteSpace(l.Titulo));

            // foreach (var livroInvalido in livrosInvalidos)
            //     yield return new($"O título do livro {livroInvalido.Ordem} não foi informado.", [nameof(Livros)]);
        }



    }
}
