using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Repositories
{
    public interface IAgendamentoRepository
    {
        List<AgendamentoManutencao> GetAgendamentos();
        List<AgendamentoManutencao> GetAgendamentosPorData(DateTime data);
        AgendamentoManutencao? GetAgendamentoById(long id);
        void AddAgendamento(AgendamentoManutencao agendamento);
        void UpdateAgendamento(AgendamentoManutencao agendamento);
        void DeleteAgendamento(long id);
    }
}