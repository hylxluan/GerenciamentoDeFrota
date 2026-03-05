using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Configs;

namespace GerenciamentoDeFrota.Data.Repositories
{
    public class CentrosCustoRepository : ICentrosCustoRepository
    {

        private readonly AppDbContext? _context;


        public void AddCentroCusto(CentrosCusto centroCusto)
        {
            throw new NotImplementedException();
        }

        public void DeleteCentroCusto(long id)
        {
            throw new NotImplementedException();
        }

        public CentrosCusto? GetCentroCustoById(long id)
        {
            throw new NotImplementedException();
        }

        public List<CentrosCusto> GetCentrosCustos() =>
            _context.CentrosCusto.OrderBy(c => c.Nome).ThenByDescending(c => c.DataCriacao).ToList();

        public void UpdateCentroCusto(CentrosCusto centroCusto)
        {
            throw new NotImplementedException();
        }
    }
}
