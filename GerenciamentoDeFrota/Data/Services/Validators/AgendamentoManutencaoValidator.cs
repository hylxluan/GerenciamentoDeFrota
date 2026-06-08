using FluentValidation;
using GerenciamentoDeFrota.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Services.Validators
{
    public class AgendamentoManutencaoValidator : AbstractValidator<AgendamentoManutencao>
    {
        public AgendamentoManutencaoValidator()
        {

            RuleFor(x => x.DataAgendamento)
                .NotNull().WithMessage("Data do agendamento é obrigatória.");

            RuleFor(x => x.HorarioAgendamento)
                .NotNull().WithMessage("Horário do agendamento é obrigatório.")
                .Must(h => h!.Value.Hour >= 0 && h.Value.Hour <= 23)
                .When(x => x.HorarioAgendamento.HasValue)
                .WithMessage("Horário inválido.");

            RuleFor(x => x.Servico)
                .NotEmpty().WithMessage("Serviço é obrigatório.")
                .MaximumLength(1000).WithMessage("Serviço: máximo 1000 caracteres.");

            RuleFor(x => x.KmAtualAgendamento)
                .NotEmpty().WithMessage("KM no momento do agendamento é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("KM não pode ser negativo.")
                .When(x => x.KmAtualAgendamento.HasValue);

            RuleFor(x => x.Observacoes)
                .MaximumLength(1000).WithMessage("Observações: máximo 1000 caracteres.")
                .When(x => x.Observacoes is not null);
        }
    }
}
