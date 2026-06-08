// ─── VeiculoDocumento.cs ────────────────────────────────────────────────────
namespace GerenciamentoDeFrota.Data.Models
{
    /// <summary>
    /// Representa a lista "Outros Documentos" do cadastro de veículo.
    /// </summary>
    public class VeiculoDocumento
    {
        public long Id { get; set; }
        public long VeiculoId { get; set; }
        public string Documento { get; set; } = string.Empty;
        public DateTime DtVencimento { get; set; }

        public Veiculos? Veiculo { get; set; }
    }
}