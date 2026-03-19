using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Interfaces.Services;

namespace GerenciamentoDeFrota.Data.Services
{
    public class ServiceVeiculos : IServiceVeiculos
    {
        private readonly IVeiculosRepository _repository;

        public ServiceVeiculos(IVeiculosRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Veiculos>> ListarVeiculosAsync() =>
            await _repository.GetVeiculosAsync();

        public async Task<Veiculos?> RecuperarVeiculoByIdAsync(long id)
        {
            return await _repository.GetVeiculoByIdAsync(id)
                ?? throw new RegisterNotFoundException("Veículo não encontrado!");
        }

        public async Task SalvarVeiculoAsync(Veiculos veiculo)
        {
            if (veiculo == null)
                throw new ArgumentNullException(nameof(veiculo));

            if (string.IsNullOrWhiteSpace(veiculo.Placa))
                throw new ErrorOnValidationException("A placa do veículo é obrigatória!");

            if (string.IsNullOrWhiteSpace(veiculo.Fabricante))
                throw new ErrorOnValidationException("O fabricante do veículo é obrigatório!");

            if (string.IsNullOrWhiteSpace(veiculo.Modelo))
                throw new ErrorOnValidationException("O modelo do veículo é obrigatório!");

            if (veiculo.Id == 0)
                await _repository.AddVeiculoAsync(veiculo);
            else
                await _repository.UpdateVeiculoAsync(veiculo);
        }

        public async Task DeletarVeiculoAsync(long id) =>
            await _repository.DeleteVeiculoAsync(id);
    }
}