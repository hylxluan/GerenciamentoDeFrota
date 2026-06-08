using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Interfaces.Repositories;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeFrota.Data.Repositories
{
    public class CentrosCustoRepository : ICentrosCustoRepository
    {
        private readonly AppDbContext _context;

        public CentrosCustoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCentroCustoAsync(CentrosCusto centroCusto)
        {
            await _context.CentrosCusto.AddAsync(centroCusto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCentroCustoAsync(CentrosCusto centroCusto)
        {
            _context.CentrosCusto.Update(centroCusto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCentroCustoAsync(long id)
        {
            var entity = await GetCentroCustoByIdAsync(id);

            if (entity is null)
                throw new RegisterNotFoundException("Centro de custo não encontrado para exclusão!");

            _context.CentrosCusto.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<CentrosCusto?> GetCentroCustoByIdAsync(long id) =>
            await _context.CentrosCusto.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<List<CentrosCusto>> GetCentrosCustosAsync() =>
            await _context.CentrosCusto
                .OrderBy(c => c.Nome)
                .ThenByDescending(c => c.DataCriacao)
                .ToListAsync();
    }
}
