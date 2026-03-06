using GerenciamentoDeFrota.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace GerenciamentoDeFrota.Interfaces.Services
{
    public interface IServiceCentrosCusto
    {
        void SalvarCentroCusto(CentrosCusto centroCusto);
        List<CentrosCusto> ListarCentrosCustos();
        CentrosCusto? RecuperarCentrosCustoById(long id);
        void DeletarCentroCusto(long id);
    }
}
