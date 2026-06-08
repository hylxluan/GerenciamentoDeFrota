using FluentValidation;
using GerenciamentoDeFrota.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GerenciamentoDeFrota.Data.Services.Validators
{
    public class VeiculoAnexoValidator : AbstractValidator<VeiculoAnexo>
    {
        private static readonly string[] _extensoesPermitidas =
            [".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx"];

        public VeiculoAnexoValidator()
        {
            RuleFor(x => x.NomeArquivo)
                .NotEmpty().WithMessage("Nome do arquivo é obrigatório.")
                .MaximumLength(500).WithMessage("Nome do arquivo: máximo 500 caracteres.")
                .Must(TerExtensaoPermitida)
                .WithMessage($"Extensão não permitida. Use: {string.Join(", ", _extensoesPermitidas)}");

            RuleFor(x => x.CaminhoArquivo)
                .NotEmpty().WithMessage("Caminho do arquivo é obrigatório.")
                .MaximumLength(1000).WithMessage("Caminho: máximo 1000 caracteres.");

            RuleFor(x => x.TipoArquivo)
                .MaximumLength(100).When(x => x.TipoArquivo is not null);

            RuleFor(x => x.TamanhoBytes)
                .GreaterThan(0).WithMessage("Arquivo não pode estar vazio.")
                .LessThanOrEqualTo(20 * 1024 * 1024L) // 20 MB
                .WithMessage("Arquivo: tamanho máximo de 20 MB.")
                .When(x => x.TamanhoBytes.HasValue);
        }

        private static bool TerExtensaoPermitida(string nomeArquivo)
        {
            var ext = Path.GetExtension(nomeArquivo).ToLowerInvariant();
            return _extensoesPermitidas.Contains(ext);
        }
    }
}
