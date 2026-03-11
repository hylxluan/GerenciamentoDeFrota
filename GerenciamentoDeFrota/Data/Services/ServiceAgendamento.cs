using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Interfaces.Services;

namespace GerenciamentoDeFrota.Data.Services
{
    public class ServiceAgendamento : IServiceAgendamento
    {
        private readonly IAgendamentoRepository _repository;

        public ServiceAgendamento(IAgendamentoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<AgendamentoManutencao> ListarAgendamentos() =>
            _repository.GetAgendamentos();

        public List<AgendamentoManutencao> ListarPorData(DateTime data) =>
            _repository.GetAgendamentosPorData(data);

        public AgendamentoManutencao? RecuperarPorId(long id) =>
            _repository.GetAgendamentoById(id) ?? throw new RegisterNotFoundException(string.Empty);

        public void SalvarAgendamento(AgendamentoManutencao agendamento)
        {
            if (agendamento == null)
                throw new ArgumentNullException(nameof(agendamento));

            if (agendamento.VeiculoId == 0)
                throw new ErrorOnValidationException("Selecione um veículo para o agendamento!");

            if (agendamento.DataAgendamento == null)
                throw new ErrorOnValidationException("A data do agendamento é obrigatória!");

            if (agendamento.HorarioAgendamento == null)
                throw new ErrorOnValidationException("O horário do agendamento é obrigatório!");

            if (string.IsNullOrWhiteSpace(agendamento.Servico))
                throw new ErrorOnValidationException("O serviço a realizar é obrigatório!");

            if (agendamento.Id == 0)
                _repository.AddAgendamento(agendamento);
            else
                _repository.UpdateAgendamento(agendamento);
        }

        public void DeletarAgendamento(long id) =>
            _repository.DeleteAgendamento(id);
    }
}