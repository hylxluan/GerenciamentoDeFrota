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

        /// <summary>Retorna quantos agendamentos estão vinculados ao veículo.</summary>
        Task<int> ContarVinculosAsync(long veiculoId);

        /// <summary>Deleta agendamentos vinculados e depois o veículo, tudo numa transação.</summary>
        Task DeletarComVinculosAsync(long veiculoId);
    }
}