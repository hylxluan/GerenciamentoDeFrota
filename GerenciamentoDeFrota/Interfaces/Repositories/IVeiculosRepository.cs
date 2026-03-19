using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Repositories
{
    public interface IVeiculosRepository
    {
        Task<List<Veiculos>> GetVeiculosAsync();
        Task<Veiculos?> GetVeiculoByIdAsync(long id);
        Task AddVeiculoAsync(Veiculos veiculo);
        Task UpdateVeiculoAsync(Veiculos veiculo);
        Task DeleteVeiculoAsync(long id);
    }
}