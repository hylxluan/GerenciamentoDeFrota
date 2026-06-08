using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Data.Services.Validators
{
    public class CentrosCustoValidator : AbstractValidator<CentrosCusto>
    {
        public CentrosCustoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(200).WithMessage("Nome: máximo 200 caracteres.");

            RuleFor(x => x.Observacoes)
                .MaximumLength(1000).WithMessage("Observações: máximo 1000 caracteres.")
                .When(x => x.Observacoes is not null);
        }
    }
}
