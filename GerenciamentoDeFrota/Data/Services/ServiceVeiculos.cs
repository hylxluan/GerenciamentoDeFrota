using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Data.Services.Validators;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Helpers;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Interfaces.Services;

namespace GerenciamentoDeFrota.Data.Services
{
    public class ServiceVeiculos : IServiceVeiculos
    {
        private readonly IVeiculosRepository _repository;

        // Validators instanciados uma vez por service — são stateless
        private static readonly VeiculosValidator _veiculoValidator = new();
        private static readonly VeiculoDocumentoValidator _documentoValidator = new();
        private static readonly VeiculoAnexoValidator _anexoValidator = new();

        public ServiceVeiculos(IVeiculosRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // ── Listagem ──────────────────────────────────────────────────────────
        public async Task<List<Veiculos>> ListarVeiculosAsync() =>
            await _repository.GetVeiculosAsync();

        public async Task<List<Veiculos>> ListarVeiculosComCentroAsync() =>
            await _repository.ListarComCentroAsync();

        // ── Busca ─────────────────────────────────────────────────────────────
        public async Task<Veiculos?> RecuperarVeiculoByIdAsync(long id) =>
            await _repository.GetVeiculoByIdAsync(id)
            ?? throw new RegisterNotFoundException("Veículo não encontrado!");

        public async Task<Veiculos?> ObterVeiculoCompletoAsync(long id) =>
            await _repository.ObterCompletoAsync(id);

        // ── Persistência ──────────────────────────────────────────────────────
        public async Task SalvarVeiculoAsync(Veiculos veiculo)
        {
            if (veiculo is null)
                throw new ArgumentNullException(nameof(veiculo));

            // Valida o veículo — acumula todos os erros antes de lançar
            ValidatorHelper.ValidarOuLancar(_veiculoValidator, veiculo);

            // Valida cada documento avulso cadastrado
            foreach (var doc in veiculo.Documentos ?? [])
                ValidatorHelper.ValidarOuLancar(_documentoValidator, doc);

            // Valida cada anexo adicionado
            foreach (var anx in veiculo.Anexos ?? [])
                ValidatorHelper.ValidarOuLancar(_anexoValidator, anx);

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