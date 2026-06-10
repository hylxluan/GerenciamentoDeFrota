// ─── CadastroVeiculoViewModel.Documentos.cs ──────────────────────────────────
// Lógica de documentos avulsos e anexos de arquivo.
// ─────────────────────────────────────────────────────────────────────────────
using GerenciamentoDeFrota.Data.Models;
using System.IO;

namespace GerenciamentoDeFrota.ViewModels
{
    public partial class CadastroVeiculoViewModel
    {
        // ── Documentos ────────────────────────────────────────────────────────
        private void AdicionarDocumento()
        {
            if (string.IsNullOrWhiteSpace(NovoDocumentoNome) || NovoDocumentoDtVencimento is null)
            {
                MensagemErro = "Informe o nome e a data de vencimento do documento.";
                return;
            }

            Documentos.Add(new VeiculoDocumento
            {
                Documento = NovoDocumentoNome.Trim(),
                DtVencimento = NovoDocumentoDtVencimento.Value
            });

            NovoDocumentoNome = string.Empty;
            NovoDocumentoDtVencimento = null;
            MensagemErro = string.Empty;
        }

        // ── Anexos ────────────────────────────────────────────────────────────
        public void AdicionarAnexo(string caminhoOrigem)
        {
            var info = new FileInfo(caminhoOrigem);
            var pasta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "EcoFrota", "Anexos", "Veiculos",
                (_veiculoEditando?.Id ?? 0).ToString());

            Directory.CreateDirectory(pasta);

            var nomeUnico = $"{Guid.NewGuid():N}_{info.Name}";
            var destino = Path.Combine(pasta, nomeUnico);
            File.Copy(caminhoOrigem, destino, overwrite: false);

            Anexos.Add(new VeiculoAnexo
            {
                NomeArquivo = info.Name,
                CaminhoArquivo = destino,
                TipoArquivo = ObterMimeType(info.Extension),
                TamanhoBytes = info.Length,
                DataUpload = DateTime.UtcNow
            });
        }

        private static string ObterMimeType(string ext) => ext.ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/octet-stream"
        };
    }
}