using FluentValidation;
using GerenciamentoDeFrota.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Services.Validators
{
    public class VeiculosValidator : AbstractValidator<Veiculos>
    {
        public VeiculosValidator()
        {
            // ── Obrigatórios ──────────────────────────────────────────────
            RuleFor(x => x.Fabricante)
                .NotEmpty().WithMessage("Fabricante é obrigatório.")
                .MaximumLength(100).WithMessage("Fabricante: máximo 100 caracteres.");

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("Modelo é obrigatório.")
                .MaximumLength(100).WithMessage("Modelo: máximo 100 caracteres.");

            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("Tipo de veículo é obrigatório.")
                .MaximumLength(100).WithMessage("Tipo de veículo: máximo 100 caracteres.");

            RuleFor(x => x.Placa)
                .NotEmpty().WithMessage("Placa é obrigatória.")
                .MaximumLength(10).WithMessage("Placa: máximo 10 caracteres.");

            RuleFor(x => x.KmAtual)
                .NotNull().WithMessage("KM atual é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("KM atual não pode ser negativo.");

            // ── Anos ──────────────────────────────────────────────────────
            RuleFor(x => x.AnoFabricacao)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .When(x => x.AnoFabricacao.HasValue)
                .WithMessage($"Ano de fabricação inválido.");

            RuleFor(x => x.AnoModelo)
                .InclusiveBetween(1900, DateTime.Now.Year + 2)
                .When(x => x.AnoModelo.HasValue)
                .WithMessage("Ano modelo inválido.");

            RuleFor(x => x.AnoEmplacamento)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .When(x => x.AnoEmplacamento.HasValue)
                .WithMessage("Ano de emplacamento inválido.");

            RuleFor(x => x.MesEmplacamento)
                .InclusiveBetween(1, 12)
                .When(x => x.MesEmplacamento.HasValue)
                .WithMessage("Mês de emplacamento inválido.");

            RuleFor(x => x.Licenciamento)
                .InclusiveBetween(2000, DateTime.Now.Year + 1)
                .When(x => x.Licenciamento.HasValue)
                .WithMessage("Ano de licenciamento inválido.");

            RuleFor(x => x.Ipva)
                .InclusiveBetween(2000, DateTime.Now.Year + 1)
                .When(x => x.Ipva.HasValue)
                .WithMessage("Ano de IPVA inválido.");

            // ── Documentos (lógica cruzada) ───────────────────────────────
            RuleFor(x => x.SeguroDtTerminoVigencia)
                .GreaterThan(x => x.SeguroDtInicioVigencia)
                .When(x => x.SeguroDtInicioVigencia.HasValue && x.SeguroDtTerminoVigencia.HasValue)
                .WithMessage("Dt. Término do seguro deve ser posterior ao Dt. Início.");

            // ── Tamanhos opcionais ────────────────────────────────────────
            RuleFor(x => x.Renavam).MaximumLength(12).When(x => x.Renavam is not null);
            RuleFor(x => x.Antt).MaximumLength(30).When(x => x.Antt is not null);
            RuleFor(x => x.ExtintorCodigo).MaximumLength(50).When(x => x.ExtintorCodigo is not null);
            RuleFor(x => x.CPF).MaximumLength(14).When(x => x.CPF is not null);
            RuleFor(x => x.CNPJ).MaximumLength(18).When(x => x.CNPJ is not null);

            // ── Decimais ──────────────────────────────────────────────────
            RuleFor(x => x.ValorFipe)
                .GreaterThan(0).When(x => x.ValorFipe.HasValue)
                .WithMessage("Valor FIPE deve ser maior que zero.");

            RuleFor(x => x.CapacidadeTanque)
                .GreaterThan(0).When(x => x.CapacidadeTanque.HasValue)
                .WithMessage("Capacidade do tanque deve ser maior que zero.");

            RuleFor(x => x.TaraKg)
                .GreaterThanOrEqualTo(0).When(x => x.TaraKg.HasValue);

            RuleFor(x => x.LotacaoKg)
                .GreaterThanOrEqualTo(0).When(x => x.LotacaoKg.HasValue);
        }
    }
}

