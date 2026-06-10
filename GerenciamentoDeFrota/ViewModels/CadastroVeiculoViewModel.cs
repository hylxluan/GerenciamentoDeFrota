using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Enums;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using GerenciamentoDeFrota.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.IO;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CadastroVeiculoViewModel : BaseViewModel
    {
        #region Services
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

        #region Listas estáticas
        public List<string> TiposVeiculo { get; } =
        [
            "Automóvel / Carro de Passeio", "Caminhonete (Pick-up)", "SUV / Utilitário Esportivo",
            "Van / Minivan", "Furgão", "Motocicleta / Moto", "Triciclo Motorizado", "Quadriciclo",
            "Micro-ônibus", "Ônibus Urbano", "Ônibus Rodoviário", "Caminhão Leve (até 7,5t)",
            "Caminhão Médio (7,5t a 16t)", "Caminhão Pesado (16t a 40t)",
            "Caminhão Extrapesado (acima de 40t)", "Cavalo Mecânico / Caminhão-Trator",
            "Caminhão Basculante", "Caminhão Betoneira", "Caminhão Tanque", "Caminhão Frigorífico",
            "Caminhão Cegonha", "Caminhão Plataforma / Prancha", "Caminhão Guincho",
            "Trator Agrícola", "Colheitadeira / Combinada", "Pulverizador Autopropelido",
            "Plantadeira", "Trator de Esteira", "Escavadeira Hidráulica", "Retroescavadeira",
            "Pá Carregadeira (Loader)", "Motoniveladora (Patrol)", "Rolo Compactador",
            "Mini Carregadeira (Bobcat / Skid Steer)", "Guindaste / Munck", "Empilhadeira",
            "Perfuratriz / Sonda", "Ambulância", "Viatura Policial", "Caminhão de Bombeiros",
            "Veículo Blindado", "Reboque", "Semirreboque / Carreta", "Outros"
        ];

        public List<string> TiposKmHora { get; } = ["Km", "Horímetro"];

        public List<string> EstadosBrasil { get; } =
        [
            "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO",
            "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR",
            "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO"
        ];
        #endregion

        #region Título
        public string Titulo => EmModoEdicao ? "Editar Veículo" : "Novo Veículo";
        #endregion

        #region Modo edição
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
        #endregion

        #region Centro de Custo
        private ObservableCollection<CentrosCusto> _centrosCusto = [];
        public ObservableCollection<CentrosCusto> CentrosCusto
        {
            get => _centrosCusto;
            set { _centrosCusto = value; OnPropertyChanged(nameof(CentrosCusto)); }
        }

        private CentrosCusto? _centrosCustoSelecionado;
        public CentrosCusto? CentrosCustoSelecionado
        {
            get => _centrosCustoSelecionado;
            set { _centrosCustoSelecionado = value; OnPropertyChanged(nameof(CentrosCustoSelecionado)); }
        }
        #endregion

        #region Documentos
        private ObservableCollection<VeiculoDocumento> _documentos = [];
        public ObservableCollection<VeiculoDocumento> Documentos
        {
            get => _documentos;
            set { _documentos = value; OnPropertyChanged(nameof(Documentos)); }
        }

        private VeiculoDocumento? _documentoSelecionado;
        public VeiculoDocumento? DocumentoSelecionado
        {
            get => _documentoSelecionado;
            set { _documentoSelecionado = value; OnPropertyChanged(nameof(DocumentoSelecionado)); }
        }

        private string _novoDocumentoNome = string.Empty;
        public string NovoDocumentoNome
        {
            get => _novoDocumentoNome;
            set { _novoDocumentoNome = value; OnPropertyChanged(nameof(NovoDocumentoNome)); }
        }

        private DateTime? _novoDocumentoDtVencimento;
        public DateTime? NovoDocumentoDtVencimento
        {
            get => _novoDocumentoDtVencimento;
            set { _novoDocumentoDtVencimento = value; OnPropertyChanged(nameof(NovoDocumentoDtVencimento)); }
        }
        #endregion

        #region Anexos
        private ObservableCollection<VeiculoAnexo> _anexos = [];
        public ObservableCollection<VeiculoAnexo> Anexos
        {
            get => _anexos;
            set { _anexos = value; OnPropertyChanged(nameof(Anexos)); }
        }
        #endregion

        #region Identificação
        private string _fabricante = string.Empty;
        public string Fabricante
        {
            get => _fabricante;
            set { _fabricante = value; OnPropertyChanged(nameof(Fabricante)); }
        }

        private string _modelo = string.Empty;
        public string Modelo
        {
            get => _modelo;
            set { _modelo = value; OnPropertyChanged(nameof(Modelo)); }
        }

        private string _placa = string.Empty;
        public string Placa
        {
            get => _placa;
            set { _placa = value; OnPropertyChanged(nameof(Placa)); }
        }

        private string _renavam = string.Empty;
        public string Renavam
        {
            get => _renavam;
            set { _renavam = value; OnPropertyChanged(nameof(Renavam)); }
        }

        private string? _tipo;
        public string? Tipo
        {
            get => _tipo;
            set { _tipo = value; OnPropertyChanged(nameof(Tipo)); }
        }

        private string _kmAtual = string.Empty;
        public string KmAtual
        {
            get => _kmAtual;
            set { _kmAtual = value; OnPropertyChanged(nameof(KmAtual)); }
        }

        private string _cor = string.Empty;
        public string Cor
        {
            get => _cor;
            set { _cor = value; OnPropertyChanged(nameof(Cor)); }
        }

        private string _numeroFrota = string.Empty;
        public string NumeroFrota
        {
            get => _numeroFrota;
            set { _numeroFrota = value; OnPropertyChanged(nameof(NumeroFrota)); }
        }

        private string? _uf;
        public string? UF
        {
            get => _uf;
            set { _uf = value; OnPropertyChanged(nameof(UF)); }
        }
        #endregion

        #region Informações Complementares
        private string _anoModelo = string.Empty;
        public string AnoModelo
        {
            get => _anoModelo;
            set { _anoModelo = value; OnPropertyChanged(nameof(AnoModelo)); }
        }

        private string _anoFabricacao = string.Empty;
        public string AnoFabricacao
        {
            get => _anoFabricacao;
            set { _anoFabricacao = value; OnPropertyChanged(nameof(AnoFabricacao)); }
        }

        private string _mesEmplacamento = string.Empty;
        public string MesEmplacamento
        {
            get => _mesEmplacamento;
            set { _mesEmplacamento = value; OnPropertyChanged(nameof(MesEmplacamento)); }
        }

        private string _anoEmplacamento = string.Empty;
        public string AnoEmplacamento
        {
            get => _anoEmplacamento;
            set { _anoEmplacamento = value; OnPropertyChanged(nameof(AnoEmplacamento)); }
        }

        private DateTime? _dataTacografo;
        public DateTime? DataTacografo
        {
            get => _dataTacografo;
            set { _dataTacografo = value; OnPropertyChanged(nameof(DataTacografo)); }
        }

        private string _kmHoraSelecionado = "Km";
        public string KmHoraSelecionado
        {
            get => _kmHoraSelecionado;
            set { _kmHoraSelecionado = value; OnPropertyChanged(nameof(KmHoraSelecionado)); }
        }

        private bool _veiculoTracao = true;
        public bool VeiculoTracao
        {
            get => _veiculoTracao;
            set { _veiculoTracao = value; OnPropertyChanged(nameof(VeiculoTracao)); }
        }

        private bool _terceirizado = false;
        public bool Terceirizado
        {
            get => _terceirizado;
            set { _terceirizado = value; OnPropertyChanged(nameof(Terceirizado)); }
        }

        private bool _ativo = true;
        public bool Ativo
        {
            get => _ativo;
            set { _ativo = value; OnPropertyChanged(nameof(Ativo)); }
        }
        #endregion

        #region Proprietário
        private string _proprietario = string.Empty;
        public string Proprietario
        {
            get => _proprietario;
            set { _proprietario = value; OnPropertyChanged(nameof(Proprietario)); }
        }

        private string _cpf = string.Empty;
        public string CPF
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(nameof(CPF)); }
        }

        private string _cnpj = string.Empty;
        public string CNPJ
        {
            get => _cnpj;
            set { _cnpj = value; OnPropertyChanged(nameof(CNPJ)); }
        }
        #endregion

        #region Dados Técnicos
        private string _valorFipe = string.Empty;
        public string ValorFipe
        {
            get => _valorFipe;
            set { _valorFipe = value; OnPropertyChanged(nameof(ValorFipe)); }
        }

        private string _capacidadeTanque = string.Empty;
        public string CapacidadeTanque
        {
            get => _capacidadeTanque;
            set { _capacidadeTanque = value; OnPropertyChanged(nameof(CapacidadeTanque)); }
        }

        private string _padronizacao = string.Empty;
        public string Padronizacao
        {
            get => _padronizacao;
            set { _padronizacao = value; OnPropertyChanged(nameof(Padronizacao)); }
        }

        private string _carroceria = string.Empty;
        public string Carroceria
        {
            get => _carroceria;
            set { _carroceria = value; OnPropertyChanged(nameof(Carroceria)); }
        }

        private string _capacidadePaletes = string.Empty;
        public string CapacidadePaletes
        {
            get => _capacidadePaletes;
            set { _capacidadePaletes = value; OnPropertyChanged(nameof(CapacidadePaletes)); }
        }

        private string _capacidadeCaixa = string.Empty;
        public string CapacidadeCaixa
        {
            get => _capacidadeCaixa;
            set { _capacidadeCaixa = value; OnPropertyChanged(nameof(CapacidadeCaixa)); }
        }

        private string _taraKg = string.Empty;
        public string TaraKg
        {
            get => _taraKg;
            set { _taraKg = value; OnPropertyChanged(nameof(TaraKg)); }
        }

        private string _lotacaoKg = string.Empty;
        public string LotacaoKg
        {
            get => _lotacaoKg;
            set { _lotacaoKg = value; OnPropertyChanged(nameof(LotacaoKg)); }
        }
        #endregion

        #region Licenciamento / IPVA / ANTT / Extintor / Cronoacógrafo
        private string _licenciamento = string.Empty;
        public string Licenciamento
        {
            get => _licenciamento;
            set { _licenciamento = value; OnPropertyChanged(nameof(Licenciamento)); }
        }

        private DateTime? _licenciamentoDtVencimento;
        public DateTime? LicenciamentoDtVencimento
        {
            get => _licenciamentoDtVencimento;
            set { _licenciamentoDtVencimento = value; OnPropertyChanged(nameof(LicenciamentoDtVencimento)); }
        }

        private string _ipva = string.Empty;
        public string Ipva
        {
            get => _ipva;
            set { _ipva = value; OnPropertyChanged(nameof(Ipva)); }
        }

        private DateTime? _ipvaDtVencimento;
        public DateTime? IpvaDtVencimento
        {
            get => _ipvaDtVencimento;
            set { _ipvaDtVencimento = value; OnPropertyChanged(nameof(IpvaDtVencimento)); }
        }

        private string _antt = string.Empty;
        public string Antt
        {
            get => _antt;
            set { _antt = value; OnPropertyChanged(nameof(Antt)); }
        }

        private DateTime? _anttDtVencimento;
        public DateTime? AnttDtVencimento
        {
            get => _anttDtVencimento;
            set { _anttDtVencimento = value; OnPropertyChanged(nameof(AnttDtVencimento)); }
        }

        private DateTime? _cronoacografoDtVencimento;
        public DateTime? CronoacografoDtVencimento
        {
            get => _cronoacografoDtVencimento;
            set { _cronoacografoDtVencimento = value; OnPropertyChanged(nameof(CronoacografoDtVencimento)); }
        }

        private DateTime? _extintorDtVencimento;
        public DateTime? ExtintorDtVencimento
        {
            get => _extintorDtVencimento;
            set { _extintorDtVencimento = value; OnPropertyChanged(nameof(ExtintorDtVencimento)); }
        }

        private string _extintorCodigo = string.Empty;
        public string ExtintorCodigo
        {
            get => _extintorCodigo;
            set { _extintorCodigo = value; OnPropertyChanged(nameof(ExtintorCodigo)); }
        }
        #endregion

        #region Seguro
        private string _seguroSeguradora = string.Empty;
        public string SeguroSeguradora
        {
            get => _seguroSeguradora;
            set { _seguroSeguradora = value; OnPropertyChanged(nameof(SeguroSeguradora)); }
        }

        private string _seguroNrApolice = string.Empty;
        public string SeguroNrApolice
        {
            get => _seguroNrApolice;
            set { _seguroNrApolice = value; OnPropertyChanged(nameof(SeguroNrApolice)); }
        }

        private DateTime? _seguroDtInicioVigencia;
        public DateTime? SeguroDtInicioVigencia
        {
            get => _seguroDtInicioVigencia;
            set { _seguroDtInicioVigencia = value; OnPropertyChanged(nameof(SeguroDtInicioVigencia)); }
        }

        private DateTime? _seguroDtTerminoVigencia;
        public DateTime? SeguroDtTerminoVigencia
        {
            get => _seguroDtTerminoVigencia;
            set { _seguroDtTerminoVigencia = value; OnPropertyChanged(nameof(SeguroDtTerminoVigencia)); }
        }

        private string _seguroTipo = string.Empty;
        public string SeguroTipo
        {
            get => _seguroTipo;
            set { _seguroTipo = value; OnPropertyChanged(nameof(SeguroTipo)); }
        }
        #endregion

        #region Observações / Erro
        private string _observacoes = string.Empty;
        public string Observacoes
        {
            get => _observacoes;
            set { _observacoes = value; OnPropertyChanged(nameof(Observacoes)); }
        }

        private string _mensagemErro = string.Empty;
        public string MensagemErro
        {
            get => _mensagemErro;
            set { _mensagemErro = value; OnPropertyChanged(nameof(MensagemErro)); }
        }
        #endregion

        #region Eventos
        public event Action? SalvoComSucesso;
        public event Action? CancelamentoSolicitado;
        public event Action? DeletarSolicitado;
        public event Action? AdicionarAnexoSolicitado;
        #endregion

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
                    UseShellExecute = true   // abre com o app padrão do SO
                });
            });

            _ = CarregarCentrosCustoAsync();

            if (_veiculoEditando is not null)
                CarregarFormulario(_veiculoEditando);
        }

        private async Task CarregarCentrosCustoAsync()
        {
            var lista = await _serviceCentrosCusto.ListarCentrosCustosAsync();
            CentrosCusto = new ObservableCollection<CentrosCusto>(lista);

            if (_veiculoEditando is not null)
                CentrosCustoSelecionado = CentrosCusto
                    .FirstOrDefault(c => c.Id == _veiculoEditando.CentrosCustoId);
        }

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

        public void AdicionarAnexo(string caminhoOrigem)
        {
            var info = new System.IO.FileInfo(caminhoOrigem);
            var pasta = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "EcoFrota", "Anexos", "Veiculos",
                (_veiculoEditando?.Id ?? 0).ToString());

            System.IO.Directory.CreateDirectory(pasta);

            var nomeUnico = $"{Guid.NewGuid():N}_{info.Name}";
            var destino = System.IO.Path.Combine(pasta, nomeUnico);
            System.IO.File.Copy(caminhoOrigem, destino, overwrite: false);

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

                // Dados técnicos
                entity.ValorFipe = decimal.TryParse(ValorFipe, NumberStyles.Any, ptBr, out var vf) ? vf : null;
                entity.CapacidadeTanque = decimal.TryParse(CapacidadeTanque, NumberStyles.Any, ptBr, out var ct) ? ct : null;
                entity.Padronizacao = Padronizacao;
                entity.Carroceria = Carroceria;
                entity.CapacidadePaletes = decimal.TryParse(CapacidadePaletes, NumberStyles.Any, ptBr, out var cp) ? cp : null;
                entity.CapacidadeCaixa = decimal.TryParse(CapacidadeCaixa, NumberStyles.Any, ptBr, out var cc) ? cc : null;
                entity.TaraKg = decimal.TryParse(TaraKg, NumberStyles.Any, ptBr, out var tk) ? tk : null;
                entity.LotacaoKg = decimal.TryParse(LotacaoKg, NumberStyles.Any, ptBr, out var lk) ? lk : null;

                // Documentos de vencimento
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

                // Coleções
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

        private void CarregarFormulario(Veiculos v)
        {
            var ptBr = new CultureInfo("pt-BR");

            Fabricante = v.Fabricante ?? string.Empty;
            Modelo = v.Modelo ?? string.Empty;
            Placa = v.Placa ?? string.Empty;
            Renavam = v.Renavam ?? string.Empty;
            Tipo = v.Tipo;
            NumeroFrota = v.NumeroFrota ?? string.Empty;
            UF = v.UF;   // null → ComboBox sem seleção
            Cor = v.Cor ?? string.Empty;

            KmAtual = v.KmAtual.HasValue
                ? v.KmAtual.Value.ToString("N0", ptBr)
                : string.Empty;

            AnoModelo = v.AnoModelo?.ToString() ?? string.Empty;
            AnoFabricacao = v.AnoFabricacao?.ToString() ?? string.Empty;
            MesEmplacamento = v.MesEmplacamento?.ToString() ?? string.Empty;
            AnoEmplacamento = v.AnoEmplacamento?.ToString() ?? string.Empty;
            DataTacografo = v.DataTacografo;
            KmHoraSelecionado = v.KmHora == EquipamentoUtiliza.Horas ? "Horímetro" : "Km";
            VeiculoTracao = v.VeiculoTracao ?? true;
            Terceirizado = v.Terceirizado ?? false;
            Ativo = v.Ativo ?? true;

            Proprietario = v.Proprietario ?? string.Empty;
            CPF = v.CPF ?? string.Empty;
            CNPJ = v.CNPJ ?? string.Empty;

            ValorFipe = v.ValorFipe?.ToString("F2", ptBr) ?? string.Empty;
            CapacidadeTanque = v.CapacidadeTanque?.ToString("F2", ptBr) ?? string.Empty;
            Padronizacao = v.Padronizacao ?? string.Empty;
            Carroceria = v.Carroceria ?? string.Empty;
            CapacidadePaletes = v.CapacidadePaletes?.ToString("F2", ptBr) ?? string.Empty;
            CapacidadeCaixa = v.CapacidadeCaixa?.ToString("F2", ptBr) ?? string.Empty;
            TaraKg = v.TaraKg?.ToString("F2", ptBr) ?? string.Empty;
            LotacaoKg = v.LotacaoKg?.ToString("F2", ptBr) ?? string.Empty;

            Licenciamento = v.Licenciamento?.ToString() ?? string.Empty;
            LicenciamentoDtVencimento = v.LicenciamentoDtVencimento;
            Ipva = v.Ipva?.ToString() ?? string.Empty;
            IpvaDtVencimento = v.IpvaDtVencimento;
            Antt = v.Antt ?? string.Empty;
            AnttDtVencimento = v.AnttDtVencimento;
            CronoacografoDtVencimento = v.CronoacografoDtVencimento;
            ExtintorDtVencimento = v.ExtintorDtVencimento;
            ExtintorCodigo = v.ExtintorCodigo ?? string.Empty;

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