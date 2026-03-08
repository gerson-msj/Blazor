using System;
using System.ComponentModel.DataAnnotations;
using Blazor.Core.Domain.Dto;

namespace Blazor.Server.Models;

public class SerieEditModel : SerieDto, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Nome))
            yield return new("Infomre o nome da série.", [nameof(Nome)]);



    }
}
