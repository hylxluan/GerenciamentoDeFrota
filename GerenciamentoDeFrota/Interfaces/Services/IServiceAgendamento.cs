using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceAgendamento
    {
        Task<List<AgendamentoManutencao>> ListarAgendamentosAsync();
        Task<List<AgendamentoManutencao>> ListarPorDataAsync(DateTime data);
        Task<AgendamentoManutencao?> RecuperarPorIdAsync(long id);
        Task SalvarAgendamentoAsync(AgendamentoManutencao agendamento);
        Task DeletarAgendamentoAsync(long id);
    }
}