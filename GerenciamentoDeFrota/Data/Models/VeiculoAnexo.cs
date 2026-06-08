// ─── VeiculoAnexo.cs ────────────────────────────────────────────────────────
namespace GerenciamentoDeFrota.Data.Models
{
    /// <summary>
    /// Representa a seção "Anexos" — armazena o caminho do arquivo no servidor/disco.
    /// </summary>
    public class VeiculoAnexo
    {
        public long Id { get; set; }
        public long VeiculoId { get; set; }
        public string NomeArquivo { get; set; } = string.Empty;
        public string CaminhoArquivo { get; set; } = string.Empty;
        public string? TipoArquivo { get; set; }
        public long? TamanhoBytes { get; set; }
        public DateTime DataUpload { get; set; } = DateTime.UtcNow;

        public Veiculos? Veiculo { get; set; }

        // campo computado
        public string TamanhoFormatado => TamanhoBytes.HasValue
            ? $"{TamanhoBytes.Value / 1024.0:N0} KB"
            : "-";
    }
}