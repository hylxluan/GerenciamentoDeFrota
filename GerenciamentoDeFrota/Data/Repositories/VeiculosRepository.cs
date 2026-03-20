using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
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

        public async Task<int> ContarVinculosAsync(long veiculoId) =>
            await _context.AgendamentosManutencao
                          .CountAsync(a => a.VeiculoId == veiculoId);

        public async Task DeleteVeiculoAsync(long id)
        {
            var entity = await GetVeiculoByIdAsync(id)
                ?? throw new RegisterNotFoundException("Veículo não encontrado para exclusão!");

            _context.Veiculos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarComVinculosAsync(long veiculoId)
        {
            var entity = await GetVeiculoByIdAsync(veiculoId)
                ?? throw new RegisterNotFoundException("Veículo não encontrado para exclusão!");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var agendamentos = await _context.AgendamentosManutencao
                    .Where(a => a.VeiculoId == veiculoId)
                    .ToListAsync();

                _context.AgendamentosManutencao.RemoveRange(agendamentos);
                await _context.SaveChangesAsync();


                _context.Veiculos.Remove(entity);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new GerenciamentoDeFrotaExceptions();
            }
        }
    }
}