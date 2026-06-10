// ─── ServiceVeiculos.cs ──────────────────────────────────────────────────────
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

        // ── Listagem ──────────────────────────────────────────────────────────
        public async Task<List<Veiculos>> ListarVeiculosAsync() =>
            await _repository.GetVeiculosAsync();

        /// <summary>
        /// Listagem com CentrosCusto incluído — usada pelo DataGrid de veículos.
        /// </summary>
        public async Task<List<Veiculos>> ListarVeiculosComCentroAsync() =>
            await _repository.ListarComCentroAsync();

        // ── Busca ─────────────────────────────────────────────────────────────
        public async Task<Veiculos?> RecuperarVeiculoByIdAsync(long id) =>
            await _repository.GetVeiculoByIdAsync(id)
            ?? throw new RegisterNotFoundException("Veículo não encontrado!");

        /// <summary>
        /// Retorna o veículo com CentrosCusto, Documentos e Anexos carregados.
        /// Deve ser usado antes de abrir o modal de edição.
        /// </summary>
        public async Task<Veiculos?> ObterVeiculoCompletoAsync(long id) =>
            await _repository.ObterCompletoAsync(id);

        // ── Persistência ──────────────────────────────────────────────────────
        public async Task SalvarVeiculoAsync(Veiculos veiculo)
        {
            if (veiculo is null)
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

        // ── Exclusão ──────────────────────────────────────────────────────────
        public async Task DeletarVeiculoAsync(long id)
        {
            var totalVinculos = await _repository.ContarVinculosAsync(id);

            if (totalVinculos > 0)
                throw new VeiculoPossuiVinculosException(totalVinculos);

            await _repository.DeleteVeiculoAsync(id);
        }

        public async Task DeletarVeiculoComVinculosAsync(long id) =>
            await _repository.DeletarComVinculosAsync(id);
    }
}