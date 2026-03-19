using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceVeiculos
    {
        Task<List<Veiculos>> ListarVeiculosAsync();
        Task<Veiculos?> RecuperarVeiculoByIdAsync(long id);
        Task SalvarVeiculoAsync(Veiculos veiculo);
        Task DeletarVeiculoAsync(long id);
    }
}