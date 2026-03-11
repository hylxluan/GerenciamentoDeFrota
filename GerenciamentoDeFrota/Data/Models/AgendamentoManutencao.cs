using System;

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
        public string? Observacoes { get; set; } = string.Empty;
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow.Date;

       
        public string VeiculoDescricao => Veiculo != null
            ? $"{Veiculo.Placa} — {Veiculo.Modelo}"
            : string.Empty;

        public string HoraFormatada => HorarioAgendamento?.ToString("HH:mm") ?? string.Empty;
    }
}