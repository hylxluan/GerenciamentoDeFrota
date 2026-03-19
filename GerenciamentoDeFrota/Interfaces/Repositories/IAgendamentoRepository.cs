using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Repositories
{
    public interface IAgendamentoRepository
    {
        Task<List<AgendamentoManutencao>> GetAgendamentosAsync();
        Task<List<AgendamentoManutencao>> GetAgendamentosPorDataAsync(DateTime data);
        Task<AgendamentoManutencao?> GetAgendamentoByIdAsync(long id);
        Task AddAgendamentoAsync(AgendamentoManutencao agendamento);
        Task UpdateAgendamentoAsync(AgendamentoManutencao agendamento);
        Task DeleteAgendamentoAsync(long id);
    }
}