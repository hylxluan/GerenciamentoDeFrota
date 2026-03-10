using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceVeiculos
    {
        List<Veiculos> ListarVeiculos();
        Veiculos? RecuperarVeiculoById(long id);
        void SalvarVeiculo(Veiculos veiculo);
        void DeletarVeiculo(long id);
    }
}