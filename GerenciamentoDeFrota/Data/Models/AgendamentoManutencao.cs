using System;
using System.Globalization;

namespace GerenciamentoDeFrota.Data.Models
{
    [Serializable]
    public class AgendamentoManutencao
    {
        public long Id { get; set; }

        // FK Veículo — obrigatório
        public long VeiculoId { get; set; }
        public Veiculos? Veiculo { get; set; }

        public DateTime? DataAgendamento { get; set; } = DateTime.UtcNow.Date;
        public DateTime? HorarioAgendamento { get; set; }
        public string? Servico { get; set; } = string.Empty;

        /// <summary>
        /// KM ou leitura de horímetro no momento do agendamento.
        /// Nullable — nem todo veículo usa KM (máquinas com horímetro, etc).
        /// </summary>
        public int? KmAtualAgendamento { get; set; }

        public string? Observacoes { get; set; } = string.Empty;
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow.Date;

        // ── Computed — NÃO mapeados pelo EF (Ignore em OnModelCreating) ──────
        public string VeiculoDescricao => Veiculo != null
            ? $"{Veiculo.Placa} — {Veiculo.Modelo}"
            : string.Empty;

        public string HoraFormatada => HorarioAgendamento?.ToString("HH:mm") ?? string.Empty;

        public string KmFormatado => KmAtualAgendamento.HasValue
            ? KmAtualAgendamento.Value.ToString("N0", new CultureInfo("pt-BR"))
            : "-";
    }
}