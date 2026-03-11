using System.Collections.Generic;

namespace GerenciamentoDeFrota.Data.Models
{
    /// <summary>
    /// Representa uma faixa de hora na timeline (ex: "08:00")
    /// com os agendamentos que caem naquele horário.
    /// </summary>
    public class AgendamentoSlot
    {
        public string HoraLabel { get; set; } = string.Empty;
        public List<AgendamentoManutencaoDisplay> Agendamentos { get; set; } = new();
    }

    /// <summary>
    /// DTO de exibição de um agendamento dentro de um slot da timeline.
    /// Evita expor a entidade do banco direto na View.
    /// </summary>
    public class AgendamentoManutencaoDisplay
    {
        public string HoraFormatada { get; set; } = string.Empty;
        public string Veiculo { get; set; } = string.Empty;
        public string TipoServico { get; set; } = string.Empty;
        public string Fornecedor { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;

        // Cor do card na timeline — hex baseado no tipo de serviço
        public string Cor { get; set; } = "#2563EB";
    }
}