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

                optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
            }
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
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.KmAtual).IsRequired();
                entity.Property(e => e.AnoModelo).IsRequired(false);
                entity.Property(e => e.AnoFabricacao).IsRequired(false);
                entity.Property(e => e.Ativo).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.Renavam).HasMaxLength(12).IsRequired(false);
                entity.Property(e => e.Placa).HasMaxLength(10).IsRequired(true);
                entity.Property(e => e.MesEmplacamento).IsRequired(false);
                entity.Property(e => e.AnoEmplacamento).IsRequired(false);
                entity.Property(e => e.DataTacografo).IsRequired(false);
                entity.Property(e => e.Cor).HasMaxLength(50).IsRequired(false);
                entity.Property(e => e.Observacoes).HasMaxLength(1000).IsRequired(false);
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Ignore(e => e.VeiculoDescricao);
            });

            modelBuilder.Entity<AgendamentoManutencao>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Veiculo)
                      .WithMany()
                      .HasForeignKey(e => e.VeiculoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.DataAgendamento).IsRequired();
                entity.Property(e => e.HorarioAgendamento).IsRequired();
                entity.Property(e => e.Servico).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.KmAtualAgendamento).IsRequired(false); // nullable — opcional
                entity.Property(e => e.Observacoes).HasMaxLength(1000).IsRequired(false);
                entity.Property(e => e.DataCriacao).IsRequired();

                // Computed — não persistidos
                entity.Ignore(e => e.VeiculoDescricao);
                entity.Ignore(e => e.HoraFormatada);
                entity.Ignore(e => e.KmFormatado);
            });
        }
    }
}