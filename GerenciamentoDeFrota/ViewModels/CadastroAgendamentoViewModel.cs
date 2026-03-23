using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using GerenciamentoDeFrota.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CadastroAgendamentoViewModel : BaseViewModel
    {
        private readonly IServiceAgendamento _serviceAgendamento;
        private readonly IServiceVeiculos _serviceVeiculos;

        // Lista completa para filtrar localmente
        private List<Veiculos> _todosVeiculos = new();

        #region Commands
        public ICommand SalvarCommand { get; set; }
        public ICommand LimparCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion

        #region Autocomplete veículo
        public ObservableCollection<Veiculos> VeiculosFiltrados { get; } = new();

        private bool _popupVeiculoAberto;
        public bool PopupVeiculoAberto
        {
            get => _popupVeiculoAberto;
            set { _popupVeiculoAberto = value; OnPropertyChanged(nameof(PopupVeiculoAberto)); }
        }

        // Evita loop circular entre TextoBuscaVeiculo ↔ VeiculoSelecionado
        private bool _selecionandoVeiculo = false;

        private string _textoBuscaVeiculo = string.Empty;
        public string TextoBuscaVeiculo
        {
            get => _textoBuscaVeiculo;
            set
            {
                _textoBuscaVeiculo = value;
                OnPropertyChanged(nameof(TextoBuscaVeiculo));

                if (_selecionandoVeiculo) return;

                // Usuário editou o texto → limpa seleção
                if (_veiculoSelecionado != null &&
                    _veiculoSelecionado.VeiculoDescricao != value)
                {
                    _veiculoSelecionado = null;
                    OnPropertyChanged(nameof(VeiculoSelecionado));
                    OnPropertyChanged(nameof(VeiculoFoiSelecionado));
                    KmAtualDisplay = string.Empty;
                }

                FiltrarVeiculos(value);
            }
        }

        private void FiltrarVeiculos(string texto)
        {
            VeiculosFiltrados.Clear();

            if (string.IsNullOrWhiteSpace(texto))
            {
                PopupVeiculoAberto = false;
                return;
            }

            foreach (var v in _todosVeiculos
                .Where(v => v.VeiculoDescricao
                             .Contains(texto, StringComparison.OrdinalIgnoreCase))
                .Take(8))
            {
                VeiculosFiltrados.Add(v);
            }

            PopupVeiculoAberto = VeiculosFiltrados.Count > 0;
        }
        #endregion

        #region Fields

        private Veiculos? _veiculoSelecionado;
        public Veiculos? VeiculoSelecionado
        {
            get => _veiculoSelecionado;
            set
            {
                _veiculoSelecionado = value;
                OnPropertyChanged(nameof(VeiculoSelecionado));
                OnPropertyChanged(nameof(VeiculoFoiSelecionado));

                if (value == null) return;

                _selecionandoVeiculo = true;
                TextoBuscaVeiculo = value.VeiculoDescricao;
                _selecionandoVeiculo = false;
                PopupVeiculoAberto = false;
            }
        }

        /// <summary>Controla Visibility do bloco KM via BooleanToVisibilityConverter.</summary>
        public bool VeiculoFoiSelecionado => _veiculoSelecionado != null;

        private DateTime? _dataAgendamento = DateTime.Today;
        public DateTime? DataAgendamento
        {
            get => _dataAgendamento;
            set { _dataAgendamento = value; OnPropertyChanged(nameof(DataAgendamento)); }
        }

        private string _horario = string.Empty;
        public string Horario
        {
            get => _horario;
            set { _horario = value; OnPropertyChanged(nameof(Horario)); }
        }

        private string _servico = string.Empty;
        public string Servico
        {
            get => _servico;
            set { _servico = value; OnPropertyChanged(nameof(Servico)); }
        }

        private string _kmAtualDisplay = string.Empty;
        public string KmAtualDisplay
        {
            get => _kmAtualDisplay;
            set { _kmAtualDisplay = value; OnPropertyChanged(nameof(KmAtualDisplay)); }
        }

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
            set
            {
                _mensagemErro = value;
                OnPropertyChanged(nameof(MensagemErro));
                OnPropertyChanged(nameof(TemErro));
            }
        }

        public bool TemErro => !string.IsNullOrWhiteSpace(_mensagemErro);

        #endregion

        public event Action? SalvoComSucesso;
        public event Action? CancelamentoSolicitado;

        public CadastroAgendamentoViewModel(
            IServiceAgendamento serviceAgendamento,
            IServiceVeiculos serviceVeiculos) : base()
        {
            _serviceAgendamento = serviceAgendamento ?? throw new ArgumentNullException(nameof(serviceAgendamento));
            _serviceVeiculos = serviceVeiculos ?? throw new ArgumentNullException(nameof(serviceVeiculos));

            SalvarCommand = new SimpleRelayCommand(async () => await SalvarAsync());
            LimparCommand = new SimpleRelayCommand(Limpar);
            CancelarCommand = new SimpleRelayCommand(Cancelar);

            _ = CarregarVeiculosAsync();
        }

        private async Task CarregarVeiculosAsync()
        {
            _todosVeiculos = await _serviceVeiculos.ListarVeiculosAsync();
        }

        private async Task SalvarAsync()
        {
            try
            {
                MensagemErro = string.Empty;

                DateTime? horarioParsed = null;
                if (!string.IsNullOrWhiteSpace(Horario) &&
                    TimeSpan.TryParse(Horario, out var ts))
                    horarioParsed = DateTime.Today.Add(ts);

                var entity = new AgendamentoManutencao
                {
                    VeiculoId = _veiculoSelecionado?.Id ?? 0,
                    DataAgendamento = DataAgendamento,
                    HorarioAgendamento = horarioParsed,
                    Servico = Servico,
                    KmAtualAgendamento = ParseKmDisplay(),
                    Observacoes = Observacoes,
                    DataCriacao = DateTime.UtcNow
                };

                await _serviceAgendamento.SalvarAgendamentoAsync(entity);
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
            _selecionandoVeiculo = true;
            TextoBuscaVeiculo = string.Empty;
            _selecionandoVeiculo = false;

            _veiculoSelecionado = null;
            OnPropertyChanged(nameof(VeiculoSelecionado));
            OnPropertyChanged(nameof(VeiculoFoiSelecionado));
            VeiculosFiltrados.Clear();
            PopupVeiculoAberto = false;

            DataAgendamento = DateTime.Today;
            Horario = string.Empty;
            Servico = string.Empty;
            KmAtualDisplay = string.Empty;
            Observacoes = string.Empty;
            MensagemErro = string.Empty;
        }

        private void Cancelar() => CancelamentoSolicitado?.Invoke();

        private int? ParseKmDisplay()
        {
            if (string.IsNullOrWhiteSpace(_kmAtualDisplay)) return null;

            var cleaned = _kmAtualDisplay
                .Replace(".", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();

            return int.TryParse(cleaned, out var valor) ? valor : null;
        }
    }
}