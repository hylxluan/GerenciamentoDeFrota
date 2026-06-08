using GerenciamentoDeFrota.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceCentrosCusto
    {
        Task SalvarCentroCustoAsync(CentrosCusto centroCusto);
        Task<List<CentrosCusto>> ListarCentrosCustosAsync();
        Task<CentrosCusto?> RecuperarCentrosCustoByIdAsync(long id);
        Task DeletarCentroCustoAsync(long id);
    }
}
