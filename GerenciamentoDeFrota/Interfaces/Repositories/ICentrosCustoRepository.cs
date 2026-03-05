using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Data.Models;

namespace GerenciamentoDeFrota.Interfaces.Repositories
{
    public interface ICentrosCustoRepository
    {
        List<CentrosCusto> GetCentrosCustos();
            CentrosCusto? GetCentroCustoById(long id);
            void AddCentroCusto(CentrosCusto centroCusto);
            void UpdateCentroCusto(CentrosCusto centroCusto);
            void DeleteCentroCusto(long id);
    }
}
