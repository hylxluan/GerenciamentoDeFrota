using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceAgendamento
    {
        List<AgendamentoManutencao> ListarAgendamentos();
        List<AgendamentoManutencao> ListarPorData(DateTime data);
        AgendamentoManutencao? RecuperarPorId(long id);
        void SalvarAgendamento(AgendamentoManutencao agendamento);
        void DeletarAgendamento(long id);
    }
}