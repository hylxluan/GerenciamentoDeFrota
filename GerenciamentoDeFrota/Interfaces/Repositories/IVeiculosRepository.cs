using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Data.Models;
namespace GerenciamentoDeFrota.Interfaces.Repositories
{
    public interface IVeiculosRepository
    {
        List<Veiculos> GetVeiculos();
        Veiculos? GetVeiculoById(long id);

        void AddVeiculo(Veiculos veiculo);
        void UpdateVeiculo(Veiculos veiculo);
        void DeleteVeiculo(long id);
    }
}
