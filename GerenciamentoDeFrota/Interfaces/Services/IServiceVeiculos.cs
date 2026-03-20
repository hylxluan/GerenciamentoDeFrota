using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceVeiculos
    {
        Task<List<Veiculos>> ListarVeiculosAsync();
        Task<Veiculos?> RecuperarVeiculoByIdAsync(long id);
        Task SalvarVeiculoAsync(Veiculos veiculo);

        /// <summary>
        /// Tenta deletar o veículo. Lança VeiculoPossuiVinculosException
        /// se houver agendamentos vinculados.
        /// </summary>
        Task DeletarVeiculoAsync(long id);

        /// <summary>
        /// Deleta o veículo e todos os registros vinculados (agendamentos, etc.)
        /// em uma única transação.
        /// </summary>
        Task DeletarVeiculoComVinculosAsync(long id);
    }
}