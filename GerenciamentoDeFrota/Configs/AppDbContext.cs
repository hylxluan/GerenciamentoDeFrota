using Microsoft.EntityFrameworkCore;
using GerenciamentoDeFrota.Data.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace GerenciamentoDeFrota.Configs
{
    public class AppDbContext : DbContext
    {
        public DbSet<CentrosCusto> CentrosCusto { get; set; }
        public DbSet<Veiculos> Veiculos { get; set; }
        public DbSet<AgendamentoManutencao> AgendamentosManutencao { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                optionsBuilder.UseMySQL(config.GetConnectionString("Default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}