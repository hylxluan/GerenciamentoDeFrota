using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using GerenciamentoDeFrota.Interfaces.Services;
using System.Globalization;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CadastroVeiculoViewModel : BaseViewModel
    {
        #region Service
        private readonly IServiceVeiculos _service;
        #endregion

        #region Commands
        public ICommand SalvarCommand { get; set; }
        public ICommand LimparCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion

        #region Tipos de Veículo
        public List<string> TiposVeiculo { get; } =
        [
            // ── Veículos Leves ──────────────────────────────────────────────
            "Automóvel / Carro de Passeio",
            "Caminhonete (Pick-up)",
            "SUV / Utilitário Esportivo",
            "Van / Minivan",
            "Furgão",

            // ── Motocicletas ─────────────────────────────────────────────────
            "Motocicleta / Moto",
            "Triciclo Motorizado",
            "Quadriciclo",

            // ── Transporte Coletivo ──────────────────────────────────────────
            "Micro-ônibus",
            "Ônibus Urbano",
            "Ônibus Rodoviário",

            // ── Caminhões ────────────────────────────────────────────────────
            "Caminhão Leve (até 7,5t)",
            "Caminhão Médio (7,5t a 16t)",
            "Caminhão Pesado (16t a 40t)",
            "Caminhão Extrapesado (acima de 40t)",
            "Cavalo Mecânico / Caminhão-Trator",
            "Caminhão Basculante",
            "Caminhão Betoneira",
            "Caminhão Tanque",
            "Caminhão Frigorífico",
            "Caminhão Cegonha",
            "Caminhão Plataforma / Prancha",
            "Caminhão Guincho",

            // ── Máquinas Agrícolas ───────────────────────────────────────────
            "Trator Agrícola",
            "Colheitadeira / Combinada",
            "Pulverizador Autopropelido",
            "Plantadeira",

            // ── Máquinas de Construção / Pesadas ─────────────────────────────
            "Trator de Esteira",
            "Escavadeira Hidráulica",
            "Retroescavadeira",
            "Pá Carregadeira (Loader)",
            "Motoniveladora (Patrol)",
            "Rolo Compactador",
            "Mini Carregadeira (Bobcat / Skid Steer)",
            "Guindaste / Munck",
            "Empilhadeira",
            "Perfuratriz / Sonda",

            // ── Veículos Especiais ───────────────────────────────────────────
            "Ambulância",
            "Viatura Policial",
            "Caminhão de Bombeiros",
            "Veículo Blindado",

            // ── Reboques ─────────────────────────────────────────────────────
            "Reboque",
            "Semirreboque / Carreta",

            // ── Outros ───────────────────────────────────────────────────────
            "Outros"
        ];
        #endregion

        #region Fields
        private Veiculos? _veiculoEditando;

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

        // Armazenado formatado (ex.: "1.234") — convertido para int? no Salvar
        private string _kmAtual = string.Empty;
        public string KmAtual
        {
            get => _kmAtual;
            set { _kmAtual = value; OnPropertyChanged(nameof(KmAtual)); }
        }

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

        private DateTime? _dataTacografo;
        public DateTime? DataTacografo
        {
            get => _dataTacografo;
            set { _dataTacografo = value; OnPropertyChanged(nameof(DataTacografo)); }
        }

        private string _cor = string.Empty;
        public string Cor
        {
            get => _cor;
            set { _cor = value; OnPropertyChanged(nameof(Cor)); }
        }

        private string _observacoes = string.Empty;
        public string Observacoes
        {
            get => _observacoes;
            set { _observacoes = value; OnPropertyChanged(nameof(Observacoes)); }
        }

        private bool _ativo = true;
        public bool Ativo
        {
            get => _ativo;
            set { _ativo = value; OnPropertyChanged(nameof(Ativo)); }
        }

        private string _mensagemErro = string.Empty;
        public string MensagemErro
        {
            get => _mensagemErro;
            set { _mensagemErro = value; OnPropertyChanged(nameof(MensagemErro)); }
        }
        #endregion

        public event Action? SalvoComSucesso;
        public event Action? CancelamentoSolicitado;

        public CadastroVeiculoViewModel(IServiceVeiculos service, Veiculos? veiculoEditando = null) : base()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _veiculoEditando = veiculoEditando;

            SalvarCommand = new SimpleRelayCommand(Salvar);
            LimparCommand = new SimpleRelayCommand(Limpar);
            CancelarCommand = new SimpleRelayCommand(Cancelar);

            if (_veiculoEditando != null)
                CarregarFormulario(_veiculoEditando);
        }

        private void Salvar()
        {
            try
            {
                MensagemErro = string.Empty;

                var entity = _veiculoEditando ?? new Veiculos();
                entity.Fabricante = Fabricante;
                entity.Modelo = Modelo;
                entity.Placa = Placa.ToUpper();
                entity.Renavam = Renavam;
                entity.Tipo = Tipo;

                // Remove separadores de milhar antes de parsear
                var kmDigits = KmAtual.Replace(".", string.Empty);
                entity.KmAtual = int.TryParse(kmDigits, out var km) ? km : null;

                entity.AnoModelo = int.TryParse(AnoModelo, out var am) ? am : null;
                entity.AnoFabricacao = int.TryParse(AnoFabricacao, out var af) ? af : null;
                entity.MesEmplacamento = int.TryParse(MesEmplacamento, out var me) ? me : null;
                entity.DataTacografo = DataTacografo;
                entity.Cor = Cor;
                entity.Observacoes = Observacoes;
                entity.Ativo = Ativo;
                entity.DataCriacao = entity.DataCriacao ?? DateTime.UtcNow;

                _service.SalvarVeiculo(entity);
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
            Fabricante = Modelo = Placa = Renavam = AnoModelo =
            AnoFabricacao = MesEmplacamento = Cor = Observacoes = KmAtual = string.Empty;
            Tipo = null;
            DataTacografo = null;
            Ativo = true;
            MensagemErro = string.Empty;
        }

        private void Cancelar() => CancelamentoSolicitado?.Invoke();

        private void CarregarFormulario(Veiculos v)
        {
            Fabricante = v.Fabricante ?? string.Empty;
            Modelo = v.Modelo ?? string.Empty;
            Placa = v.Placa ?? string.Empty;
            Renavam = v.Renavam ?? string.Empty;
            Tipo = v.Tipo;

            // Formata o KM com separador de milhar pt-BR ao carregar
            KmAtual = v.KmAtual.HasValue
                ? v.KmAtual.Value.ToString("N0", new CultureInfo("pt-BR"))
                : string.Empty;

            AnoModelo = v.AnoModelo?.ToString() ?? string.Empty;
            AnoFabricacao = v.AnoFabricacao?.ToString() ?? string.Empty;
            MesEmplacamento = v.MesEmplacamento?.ToString() ?? string.Empty;
            DataTacografo = v.DataTacografo;
            Cor = v.Cor ?? string.Empty;
            Observacoes = v.Observacoes ?? string.Empty;
            Ativo = v.Ativo ?? true;
        }
    }
}