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

        public List<Veiculos> ListarVeiculos() => _repository.GetVeiculos();

        public Veiculos? RecuperarVeiculoById(long id)
        {
            return _repository.GetVeiculoById(id) ?? throw new RegisterNotFoundException(string.Empty);
        }

        public void SalvarVeiculo(Veiculos veiculo)
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
                _repository.AddVeiculo(veiculo);
            else
                _repository.UpdateVeiculo(veiculo);
        }

        public void DeletarVeiculo(long id) => _repository.DeleteVeiculo(id);
    }
}