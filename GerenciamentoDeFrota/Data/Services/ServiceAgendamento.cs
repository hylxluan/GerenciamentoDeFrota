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

        public async Task<List<AgendamentoManutencao>> ListarAgendamentosAsync() =>
            await _repository.GetAgendamentosAsync();

        public async Task<List<AgendamentoManutencao>> ListarPorDataAsync(DateTime data) =>
            await _repository.GetAgendamentosPorDataAsync(data);

        public async Task<AgendamentoManutencao?> RecuperarPorIdAsync(long id) =>
            await _repository.GetAgendamentoByIdAsync(id)
            ?? throw new RegisterNotFoundException(string.Empty);

        public async Task SalvarAgendamentoAsync(AgendamentoManutencao agendamento)
        {
            if (agendamento is null)
                throw new ArgumentNullException(nameof(agendamento));

            if (agendamento.VeiculoId == 0)
                throw new ErrorOnValidationException("Selecione um veículo para o agendamento!");

            if (agendamento.DataAgendamento is null)
                throw new ErrorOnValidationException("A data do agendamento é obrigatória!");

            if (agendamento.HorarioAgendamento is null)
                throw new ErrorOnValidationException("O horário do agendamento é obrigatório!");

            if (string.IsNullOrWhiteSpace(agendamento.Servico))
                throw new ErrorOnValidationException("O serviço a realizar é obrigatório!");

            if (agendamento.Id == 0)
                await _repository.AddAgendamentoAsync(agendamento);
            else
                await _repository.UpdateAgendamentoAsync(agendamento);
        }

        public async Task DeletarAgendamentoAsync(long id) =>
            await _repository.DeleteAgendamentoAsync(id);
    }
}