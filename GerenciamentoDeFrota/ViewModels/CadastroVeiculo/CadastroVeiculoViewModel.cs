// ─── CadastroVeiculoViewModel.cs ─────────────────────────────────────────────
// Núcleo: serviços, commands, construtor, eventos e ações simples.
// Os demais arquivos completam a classe via partial:
//   · .Properties.cs  — todas as propriedades bindáveis
//   · .Listas.cs      — listas estáticas (TiposVeiculo, EstadosBrasil…)
//   · .Persistencia.cs — SalvarAsync + CarregarFormulario
//   · .Documentos.cs  — AdicionarDocumento, AdicionarAnexo, ObterMimeType
// ─────────────────────────────────────────────────────────────────────────────
using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public partial class CadastroVeiculoViewModel : BaseViewModel
    {
        #region Serviços
        private readonly IServiceVeiculos _service;
        private readonly IServiceCentrosCusto _serviceCentrosCusto;
        #endregion

        #region Commands
        public ICommand SalvarCommand { get; set; }
        public ICommand LimparCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand DeletarCommand { get; set; }
        public ICommand AdicionarDocumentoCommand { get; set; }
        public ICommand RemoverDocumentoCommand { get; set; }
        public ICommand AdicionarAnexoCommand { get; set; }
        public ICommand RemoverAnexoCommand { get; set; }
        public ICommand AbrirAnexoCommand { get; set; }
        #endregion

        #region Modo edição / Título
        private Veiculos? _veiculoEditando;
        private bool _emModoEdicao;

        public bool EmModoEdicao
        {
            get => _emModoEdicao;
            set
            {
                _emModoEdicao = value;
                OnPropertyChanged(nameof(EmModoEdicao));
                OnPropertyChanged(nameof(Titulo));
            }
        }

        public string Titulo => EmModoEdicao ? "Editar Veículo" : "Novo Veículo";
        #endregion

        #region Eventos
        public event Action? SalvoComSucesso;
        public event Action? CancelamentoSolicitado;
        public event Action? DeletarSolicitado;
        public event Action? AdicionarAnexoSolicitado;
        #endregion

        // ─────────────────────────────────────────────────────────────────────
        public CadastroVeiculoViewModel(
            IServiceVeiculos service,
            IServiceCentrosCusto serviceCentrosCusto,
            Veiculos? veiculoEditando = null) : base()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _serviceCentrosCusto = serviceCentrosCusto ?? throw new ArgumentNullException(nameof(serviceCentrosCusto));
            _veiculoEditando = veiculoEditando;
            EmModoEdicao = veiculoEditando is not null;

            SalvarCommand = new SimpleRelayCommand(async () => await SalvarAsync());
            LimparCommand = new SimpleRelayCommand(Limpar);
            CancelarCommand = new SimpleRelayCommand(Cancelar);
            DeletarCommand = new SimpleRelayCommand(() => DeletarSolicitado?.Invoke());

            AdicionarDocumentoCommand = new SimpleRelayCommand(AdicionarDocumento);
            RemoverDocumentoCommand = new RelayCommands<VeiculoDocumento>(
                doc => { if (doc is not null) Documentos.Remove(doc); });

            AdicionarAnexoCommand = new SimpleRelayCommand(() => AdicionarAnexoSolicitado?.Invoke());
            RemoverAnexoCommand = new RelayCommands<VeiculoAnexo>(
                anx => { if (anx is not null) Anexos.Remove(anx); });

            AbrirAnexoCommand = new RelayCommands<VeiculoAnexo>(anx =>
            {
                if (anx is null || !System.IO.File.Exists(anx.CaminhoArquivo)) return;
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = anx.CaminhoArquivo,
                    UseShellExecute = true
                });
            });

            _ = CarregarCentrosCustoAsync();

            if (_veiculoEditando is not null)
                CarregarFormulario(_veiculoEditando);
        }

        // ─────────────────────────────────────────────────────────────────────
        private async Task CarregarCentrosCustoAsync()
        {
            var lista = await _serviceCentrosCusto.ListarCentrosCustosAsync();
            CentrosCusto = new ObservableCollection<CentrosCusto>(lista);

            if (_veiculoEditando is not null)
                CentrosCustoSelecionado = CentrosCusto
                    .FirstOrDefault(c => c.Id == _veiculoEditando.CentrosCustoId);
        }

        private void Limpar()
        {
            _veiculoEditando = null;
            EmModoEdicao = false;

            Fabricante = Modelo = Placa = Renavam = Cor = NumeroFrota =
            KmAtual = AnoModelo = AnoFabricacao = MesEmplacamento = AnoEmplacamento =
            Proprietario = CPF = CNPJ =
            ValorFipe = CapacidadeTanque = Padronizacao = Carroceria =
            CapacidadePaletes = CapacidadeCaixa = TaraKg = LotacaoKg =
            Licenciamento = Ipva = Antt = ExtintorCodigo =
            SeguroSeguradora = SeguroNrApolice = SeguroTipo =
            NovoDocumentoNome = Observacoes = string.Empty;

            DataTacografo = LicenciamentoDtVencimento = IpvaDtVencimento =
            AnttDtVencimento = CronoacografoDtVencimento =
            ExtintorDtVencimento = SeguroDtInicioVigencia =
            SeguroDtTerminoVigencia = NovoDocumentoDtVencimento = null;

            Tipo = null;
            UF = null;
            KmHoraSelecionado = "Km";
            VeiculoTracao = true;
            Terceirizado = false;
            Ativo = true;
            CentrosCustoSelecionado = null;

            Documentos.Clear();
            Anexos.Clear();
            MensagemErro = string.Empty;
        }

        private void Cancelar() => CancelamentoSolicitado?.Invoke();

        public long GetIdEditando() => _veiculoEditando?.Id ?? 0;
    }
}