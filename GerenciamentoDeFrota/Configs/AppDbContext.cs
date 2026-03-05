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
        }

    }
}
