using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Repositories
{
    public class VeiculosRepository : IVeiculosRepository
    {
        private readonly AppDbContext _context;

        public VeiculosRepository(AppDbContext context) 
        { 
            this._context = context;
        } 

        public void AddVeiculo(Veiculos veiculo)
        {
            this._context.Veiculos.Add(veiculo);
            this._context.SaveChanges();
        }

        public void DeleteVeiculo(long id)
        {
            var entity = GetVeiculoById(id);
            if (entity != null)
            {
                this._context.Veiculos.Remove(entity);
                this._context.SaveChanges();

            }
            else 
            {
                throw new RegisterNotFoundException("Veículo não encontrado para exclusão!");
            }
        }

        public Veiculos? GetVeiculoById(long id) => 
            this._context.Veiculos.FirstOrDefault(e => e.Id == id);

        public List<Veiculos> GetVeiculos() => 
            this._context.Veiculos.OrderByDescending(v => v.DataCriacao).ToList();

        public void UpdateVeiculo(Veiculos veiculo)
        {
            this._context.Veiculos.Update(veiculo);
            this._context.SaveChanges();
        }
    }
}
