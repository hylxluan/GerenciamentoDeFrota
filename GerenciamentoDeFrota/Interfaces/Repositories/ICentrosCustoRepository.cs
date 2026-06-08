using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Repositories
{
    public interface ICentrosCustoRepository
    {
        Task<List<CentrosCusto>> GetCentrosCustosAsync();
        Task<CentrosCusto?> GetCentroCustoByIdAsync(long id);
        Task AddCentroCustoAsync(CentrosCusto centroCusto);
        Task UpdateCentroCustoAsync(CentrosCusto centroCusto);
        Task DeleteCentroCustoAsync(long id);
    }
}
