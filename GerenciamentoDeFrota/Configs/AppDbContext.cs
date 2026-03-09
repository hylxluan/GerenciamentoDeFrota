using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Data.Models;
using Microsoft.Extensions.Configuration;
namespace GerenciamentoDeFrota.Configs
{
    public class AppDbContext : DbContext
    {

        public DbSet<CentrosCusto> CentrosCusto { get; set; }
        public DbSet<Veiculos> Veiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CentrosCusto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Observacoes).HasMaxLength(1000).IsRequired(false);
                entity.Property(e => e.Ativo).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.DataCriacao).IsRequired();
            });

            modelBuilder.Entity<Veiculos>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fabricante).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AnoModelo).IsRequired(false).HasMaxLength(4);
                entity.Property(e => e.AnoFabricacao).IsRequired(false).HasMaxLength(4);
                entity.Property(e => e.Ativo).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.Renavam).HasMaxLength(12).IsRequired(false);
                entity.Property(e => e.Placa).HasMaxLength(10).IsRequired(true);
                entity.Property(e => e.MesEmplacamento).IsRequired(false);
                entity.Property(e => e.AnoEmplacamento).IsRequired(false).HasMaxLength(4);
                entity.Property(e => e.DataTacografo).IsRequired(false);
                entity.Property(e => e.Cor).HasMaxLength(50).IsRequired(false);
                entity.Property(e => e.Observacoes).HasMaxLength(1000).IsRequired(false);
                entity.Property(e => e.DataCriacao).IsRequired();
            });
        }

    }
}
