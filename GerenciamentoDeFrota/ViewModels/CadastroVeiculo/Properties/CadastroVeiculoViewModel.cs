// ─── CadastroVeiculoViewModel.Properties.cs ──────────────────────────────────
// Todas as propriedades bindáveis, agrupadas por seção do formulário.
// ─────────────────────────────────────────────────────────────────────────────
using GerenciamentoDeFrota.Data.Models;
using System.Collections.ObjectModel;

namespace GerenciamentoDeFrota.ViewModels
{
    public partial class CadastroVeiculoViewModel
    {
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

        private bool _terceirizado;
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

        #region Licenciamento / IPVA / ANTT / Tacógrafo / Extintor / Cronoacógrafo
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

        #region Observações / Mensagem de Erro
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
    }
}