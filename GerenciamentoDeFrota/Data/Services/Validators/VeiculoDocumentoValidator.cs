using FluentValidation;
using GerenciamentoDeFrota.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Services.Validators
{
    public class VeiculoDocumentoValidator : AbstractValidator<VeiculoDocumento>
    {
        public VeiculoDocumentoValidator()
        {
            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("Nome do documento é obrigatório.")
                .MaximumLength(200).WithMessage("Documento: máximo 200 caracteres.");

            RuleFor(x => x.DtVencimento)
                .NotEmpty().WithMessage("Data de vencimento é obrigatória.")
                .GreaterThan(DateTime.MinValue).WithMessage("Data de vencimento inválida.");
        }
    }
}
