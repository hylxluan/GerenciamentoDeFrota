using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeFrota.Data.Repositories
{
    public class VeiculosRepository : IVeiculosRepository
    {
        private readonly AppDbContext _context;

        public VeiculosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Veiculos>> GetVeiculosAsync() =>
            await _context.Veiculos
                          .OrderByDescending(v => v.DataCriacao)
                          .ToListAsync();

        public async Task<Veiculos?> GetVeiculoByIdAsync(long id) =>
            await _context.Veiculos.FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddVeiculoAsync(Veiculos veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVeiculoAsync(Veiculos veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVeiculoAsync(long id)
        {
            var entity = await GetVeiculoByIdAsync(id);

            if (entity != null)
            {
                _context.Veiculos.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new RegisterNotFoundException("Veículo não encontrado para exclusão!");
            }
        }
    }
}