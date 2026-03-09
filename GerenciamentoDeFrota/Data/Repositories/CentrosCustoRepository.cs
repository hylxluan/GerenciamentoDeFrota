using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;

namespace GerenciamentoDeFrota.Data.Repositories
{
    public class CentrosCustoRepository : ICentrosCustoRepository
    {

        private readonly AppDbContext _context;

        public CentrosCustoRepository(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public void AddCentroCusto(CentrosCusto centroCusto)
        {
            this._context.CentrosCusto.Add(centroCusto);
            this._context.SaveChanges();
        }

        public void DeleteCentroCusto(long id)
        {
            var entity = GetCentroCustoById(id);
            if (entity != null) 
            {
                this._context.CentrosCusto.Remove(entity);
                this._context.SaveChanges();
            }
            else
            {
                throw new RegisterNotFoundException("Centro de custo não encontrado para exclusão!");
            }

        }

        public CentrosCusto? GetCentroCustoById(long id) => 
            this._context.CentrosCusto.FirstOrDefault(c => c.Id == id);

        public List<CentrosCusto> GetCentrosCustos() =>
            this._context.CentrosCusto.OrderBy(c => c.Nome).ThenByDescending(c => c.DataCriacao).ToList();

        public void UpdateCentroCusto(CentrosCusto centroCusto)
        {
            this._context.CentrosCusto.Update(centroCusto);
            this._context.SaveChanges();
        }

        
    }
}
