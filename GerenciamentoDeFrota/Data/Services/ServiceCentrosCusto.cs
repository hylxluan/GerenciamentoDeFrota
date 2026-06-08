// ─── ServiceCentrosCusto.cs ──────────────────────────────────────────────────
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Interfaces.Services;

namespace GerenciamentoDeFrota.Data.Services
{
    public class ServiceCentrosCusto : IServiceCentrosCusto
    {
        private readonly ICentrosCustoRepository _repository;

        #region Construtor
        public ServiceCentrosCusto(ICentrosCustoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #endregion

        #region Transações DB
        public async Task<List<CentrosCusto>> ListarCentrosCustosAsync() =>
            await _repository.GetCentrosCustosAsync();

        public async Task<CentrosCusto?> RecuperarCentrosCustoByIdAsync(long id)
        {
            var resultado = await _repository.GetCentroCustoByIdAsync(id);
            return resultado ?? throw new RegisterNotFoundException(string.Empty);
        }

        public async Task SalvarCentroCustoAsync(CentrosCusto centroCusto)
        {
            if (centroCusto is null)
                throw new ArgumentNullException(nameof(centroCusto), "O centro de custo não pode ser nulo!");

            if (string.IsNullOrWhiteSpace(centroCusto.Nome))
                throw new ErrorOnValidationException("O nome do centro de custo é obrigatório!");

            if (centroCusto.Id == 0)
                await _repository.AddCentroCustoAsync(centroCusto);
            else
                await _repository.UpdateCentroCustoAsync(centroCusto);
        }

        public async Task DeletarCentroCustoAsync(long id) =>
            await _repository.DeleteCentroCustoAsync(id);
        #endregion
    }
}