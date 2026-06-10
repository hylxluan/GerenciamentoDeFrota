// ─── CadastroVeiculoViewModel.Persistencia.cs ────────────────────────────────
// SalvarAsync: monta a entidade e persiste via service.
// CarregarFormulario: popula o formulário a partir de um Veiculos existente.
// ─────────────────────────────────────────────────────────────────────────────
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Enums;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace GerenciamentoDeFrota.ViewModels
{
    public partial class CadastroVeiculoViewModel
    {
        // ── Salvar ────────────────────────────────────────────────────────────
        private async Task SalvarAsync()
        {
            try
            {
                MensagemErro = string.Empty;
                var ptBr = new CultureInfo("pt-BR");

                var entity = _veiculoEditando ?? new Veiculos();

                // Identificação
                entity.Fabricante = Fabricante;
                entity.Modelo = Modelo;
                entity.Placa = Placa.ToUpper();
                entity.Renavam = Renavam;
                entity.Tipo = Tipo;
                entity.NumeroFrota = NumeroFrota;
                entity.UF = UF?.ToUpper();
                entity.Cor = Cor;

                var kmDigits = KmAtual.Replace(".", string.Empty);
                entity.KmAtual = int.TryParse(kmDigits, out var km) ? km : null;

                // Informações complementares
                entity.AnoModelo = int.TryParse(AnoModelo, out var am) ? am : null;
                entity.AnoFabricacao = int.TryParse(AnoFabricacao, out var af) ? af : null;
                entity.MesEmplacamento = int.TryParse(MesEmplacamento, out var me) ? me : null;
                entity.AnoEmplacamento = int.TryParse(AnoEmplacamento, out var ae) ? ae : null;
                entity.DataTacografo = DataTacografo;
                entity.KmHora = KmHoraSelecionado == "Horímetro"
                    ? EquipamentoUtiliza.Horas
                    : EquipamentoUtiliza.Km;
                entity.VeiculoTracao = VeiculoTracao;
                entity.Terceirizado = Terceirizado;
                entity.Ativo = Ativo;
                entity.CentrosCustoId = CentrosCustoSelecionado?.Id;

                // Proprietário
                entity.Proprietario = Proprietario;
                entity.CPF = CPF;
                entity.CNPJ = CNPJ;

                // Dados técnicos — NumberStyles.Any aceita "R$ 1.234,56" e "1.234,56"
                entity.ValorFipe = decimal.TryParse(ValorFipe, NumberStyles.Any, ptBr, out var vf) ? vf : null;
                entity.CapacidadeTanque = decimal.TryParse(CapacidadeTanque, NumberStyles.Any, ptBr, out var ct) ? ct : null;
                entity.Padronizacao = Padronizacao;
                entity.Carroceria = Carroceria;
                entity.CapacidadePaletes = decimal.TryParse(CapacidadePaletes, NumberStyles.Any, ptBr, out var cp) ? cp : null;
                entity.CapacidadeCaixa = decimal.TryParse(CapacidadeCaixa, NumberStyles.Any, ptBr, out var cc) ? cc : null;
                entity.TaraKg = decimal.TryParse(TaraKg, NumberStyles.Any, ptBr, out var tk) ? tk : null;
                entity.LotacaoKg = decimal.TryParse(LotacaoKg, NumberStyles.Any, ptBr, out var lk) ? lk : null;

                // Documentos e vencimentos
                entity.Licenciamento = int.TryParse(Licenciamento, out var lic) ? lic : null;
                entity.LicenciamentoDtVencimento = LicenciamentoDtVencimento;
                entity.Ipva = int.TryParse(Ipva, out var ipva) ? ipva : null;
                entity.IpvaDtVencimento = IpvaDtVencimento;
                entity.Antt = Antt;
                entity.AnttDtVencimento = AnttDtVencimento;
                entity.CronoacografoDtVencimento = CronoacografoDtVencimento;
                entity.ExtintorDtVencimento = ExtintorDtVencimento;
                entity.ExtintorCodigo = ExtintorCodigo;

                // Seguro
                entity.SeguroSeguradora = SeguroSeguradora;
                entity.SeguroNrApolice = SeguroNrApolice;
                entity.SeguroDtInicioVigencia = SeguroDtInicioVigencia;
                entity.SeguroDtTerminoVigencia = SeguroDtTerminoVigencia;
                entity.SeguroTipo = SeguroTipo;

                // Coleções e metadados
                entity.Documentos = [.. Documentos];
                entity.Anexos = [.. Anexos];
                entity.Observacoes = Observacoes;
                entity.DataCriacao = entity.DataCriacao ?? DateTime.UtcNow;

                await _service.SalvarVeiculoAsync(entity);
                SalvoComSucesso?.Invoke();
            }
            catch (GerenciamentoDeFrotaExceptions ex)
            {
                MensagemErro = ex.Message;
            }
            catch (Exception)
            {
                MensagemErro = "Erro inesperado ao salvar. Contate o suporte.";
            }
        }

        // ── Carregar formulário (modo edição) ─────────────────────────────────
        private void CarregarFormulario(Veiculos v)
        {
            var ptBr = new CultureInfo("pt-BR");

            // Identificação
            Fabricante = v.Fabricante ?? string.Empty;
            Modelo = v.Modelo ?? string.Empty;
            Placa = v.Placa ?? string.Empty;
            Renavam = v.Renavam ?? string.Empty;
            Tipo = v.Tipo;
            NumeroFrota = v.NumeroFrota ?? string.Empty;
            UF = v.UF;
            Cor = v.Cor ?? string.Empty;

            KmAtual = v.KmAtual.HasValue
                ? v.KmAtual.Value.ToString("N0", ptBr)
                : string.Empty;

            // Informações complementares
            AnoModelo = v.AnoModelo?.ToString() ?? string.Empty;
            AnoFabricacao = v.AnoFabricacao?.ToString() ?? string.Empty;
            MesEmplacamento = v.MesEmplacamento?.ToString() ?? string.Empty;
            AnoEmplacamento = v.AnoEmplacamento?.ToString() ?? string.Empty;
            DataTacografo = v.DataTacografo;
            KmHoraSelecionado = v.KmHora == EquipamentoUtiliza.Horas ? "Horímetro" : "Km";
            VeiculoTracao = v.VeiculoTracao ?? true;
            Terceirizado = v.Terceirizado ?? false;
            Ativo = v.Ativo ?? true;

            // Proprietário
            Proprietario = v.Proprietario ?? string.Empty;
            CPF = v.CPF ?? string.Empty;
            CNPJ = v.CNPJ ?? string.Empty;

            // Dados técnicos — "N2" para acionar a máscara monetária corretamente no TextChanged
            ValorFipe = v.ValorFipe?.ToString("N2", ptBr) ?? string.Empty;
            CapacidadeTanque = v.CapacidadeTanque?.ToString("N2", ptBr) ?? string.Empty;
            Padronizacao = v.Padronizacao ?? string.Empty;
            Carroceria = v.Carroceria ?? string.Empty;
            CapacidadePaletes = v.CapacidadePaletes?.ToString("N2", ptBr) ?? string.Empty;
            CapacidadeCaixa = v.CapacidadeCaixa?.ToString("N2", ptBr) ?? string.Empty;
            TaraKg = v.TaraKg?.ToString("N2", ptBr) ?? string.Empty;
            LotacaoKg = v.LotacaoKg?.ToString("N2", ptBr) ?? string.Empty;

            // Documentos e vencimentos
            Licenciamento = v.Licenciamento?.ToString() ?? string.Empty;
            LicenciamentoDtVencimento = v.LicenciamentoDtVencimento;
            Ipva = v.Ipva?.ToString() ?? string.Empty;
            IpvaDtVencimento = v.IpvaDtVencimento;
            Antt = v.Antt ?? string.Empty;
            AnttDtVencimento = v.AnttDtVencimento;
            CronoacografoDtVencimento = v.CronoacografoDtVencimento;
            ExtintorDtVencimento = v.ExtintorDtVencimento;
            ExtintorCodigo = v.ExtintorCodigo ?? string.Empty;

            // Seguro
            SeguroSeguradora = v.SeguroSeguradora ?? string.Empty;
            SeguroNrApolice = v.SeguroNrApolice ?? string.Empty;
            SeguroDtInicioVigencia = v.SeguroDtInicioVigencia;
            SeguroDtTerminoVigencia = v.SeguroDtTerminoVigencia;
            SeguroTipo = v.SeguroTipo ?? string.Empty;

            Observacoes = v.Observacoes ?? string.Empty;

            Documentos = new ObservableCollection<VeiculoDocumento>(v.Documentos ?? []);

            // Exibe apenas anexos cujo arquivo ainda existe no disco
            Anexos = new ObservableCollection<VeiculoAnexo>(
                (v.Anexos ?? []).Where(a => File.Exists(a.CaminhoArquivo)));
        }
    }
}