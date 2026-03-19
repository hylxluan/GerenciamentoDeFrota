using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeFrota.Data.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<AgendamentoManutencao>> GetAgendamentosAsync() =>
            await _context.AgendamentosManutencao
                .Include(a => a.Veiculo)
                .OrderBy(a => a.DataAgendamento)
                .ThenBy(a => a.HorarioAgendamento)
                .ToListAsync();

        public async Task<List<AgendamentoManutencao>> GetAgendamentosPorDataAsync(DateTime data) =>
            await _context.AgendamentosManutencao
                .Include(a => a.Veiculo)
                .Where(a => a.DataAgendamento.HasValue &&
                            a.DataAgendamento.Value.Date == data.Date)
                .OrderBy(a => a.HorarioAgendamento)
                .ToListAsync();

        public async Task<AgendamentoManutencao?> GetAgendamentoByIdAsync(long id) =>
            await _context.AgendamentosManutencao
                .Include(a => a.Veiculo)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAgendamentoAsync(AgendamentoManutencao agendamento)
        {
            _context.AgendamentosManutencao.Add(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAgendamentoAsync(AgendamentoManutencao agendamento)
        {
            _context.AgendamentosManutencao.Update(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAgendamentoAsync(long id)
        {
            var registro = await _context.AgendamentosManutencao
                               .FirstOrDefaultAsync(a => a.Id == id)
                           ?? throw new RegisterNotFoundException(string.Empty);

            _context.AgendamentosManutencao.Remove(registro);
            await _context.SaveChangesAsync();
        }
    }
}