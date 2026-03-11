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

        public List<AgendamentoManutencao> GetAgendamentos()
        {
            return _context.AgendamentosManutencao
                .Include(a => a.Veiculo)
                .OrderBy(a => a.DataAgendamento)
                .ThenBy(a => a.HorarioAgendamento)
                .ToList();
        }

        public List<AgendamentoManutencao> GetAgendamentosPorData(DateTime data)
        {
            return _context.AgendamentosManutencao
                .Include(a => a.Veiculo)
                .Where(a => a.DataAgendamento.HasValue &&
                            a.DataAgendamento.Value.Date == data.Date)
                .OrderBy(a => a.HorarioAgendamento)
                .ToList();
        }

        public AgendamentoManutencao? GetAgendamentoById(long id)
        {
            return _context.AgendamentosManutencao
                .Include(a => a.Veiculo)
                .FirstOrDefault(a => a.Id == id);
        }

        public void AddAgendamento(AgendamentoManutencao agendamento)
        {
            if (agendamento == null)
                throw new ArgumentNullException(nameof(agendamento));

            _context.AgendamentosManutencao.Add(agendamento);
            _context.SaveChanges();
        }

        public void UpdateAgendamento(AgendamentoManutencao agendamento)
        {
            if (agendamento == null)
                throw new ArgumentNullException(nameof(agendamento));

            _context.AgendamentosManutencao.Update(agendamento);
            _context.SaveChanges();
        }

        public void DeleteAgendamento(long id)
        {
            var registro = _context.AgendamentosManutencao.FirstOrDefault(a => a.Id == id)
                ?? throw new RegisterNotFoundException(string.Empty);

            _context.AgendamentosManutencao.Remove(registro);
            _context.SaveChanges();
        }
    }
}