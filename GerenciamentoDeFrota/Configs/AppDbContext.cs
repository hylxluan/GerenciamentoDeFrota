using Microsoft.EntityFrameworkCore;
using GerenciamentoDeFrota.Data.Models;
using Microsoft.Extensions.Configuration;

namespace GerenciamentoDeFrota.Configs
{
    public class AppDbContext : DbContext
    {
        public DbSet<CentrosCusto> CentrosCusto { get; set; }
        public DbSet<Veiculos> Veiculos { get; set; }
        public DbSet<VeiculoDocumento> VeiculosDocumentos { get; set; }
        public DbSet<VeiculoAnexo> VeiculosAnexos { get; set; }
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

                optionsBuilder.UseMySQL(config.GetConnectionString("Default")!);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // não apaga o centro se tiver veículo vinculado
            modelBuilder.Entity<Veiculos>()
                .HasOne(v => v.CentrosCusto)
                .WithMany(c => c.Veiculos)
                .HasForeignKey(v => v.CentrosCustoId)
                .OnDelete(DeleteBehavior.Restrict);

            // apaga os documentos junto com o veículo
            modelBuilder.Entity<VeiculoDocumento>()
                .HasOne(d => d.Veiculo)
                .WithMany(v => v.Documentos)
                .HasForeignKey(d => d.VeiculoId)
                .OnDelete(DeleteBehavior.Cascade);

            // apaga os anexos junto com o veículo
            modelBuilder.Entity<VeiculoAnexo>()
                .HasOne(a => a.Veiculo)
                .WithMany(v => v.Anexos)
                .HasForeignKey(a => a.VeiculoId)
                .OnDelete(DeleteBehavior.Cascade);

            // não apaga o veículo se tiver agendamento
            modelBuilder.Entity<AgendamentoManutencao>()
                .HasOne(a => a.Veiculo)
                .WithMany()
                .HasForeignKey(a => a.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // não persiste
            modelBuilder.Entity<Veiculos>().Ignore(v => v.VeiculoDescricao);
            modelBuilder.Entity<AgendamentoManutencao>().Ignore(a => a.VeiculoDescricao);
            modelBuilder.Entity<AgendamentoManutencao>().Ignore(a => a.HoraFormatada);
            modelBuilder.Entity<AgendamentoManutencao>().Ignore(a => a.KmFormatado);
        }
    }
}