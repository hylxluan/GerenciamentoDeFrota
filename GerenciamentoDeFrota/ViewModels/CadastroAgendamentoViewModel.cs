using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using GerenciamentoDeFrota.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CadastroAgendamentoViewModel : BaseViewModel
    {
        private readonly IServiceAgendamento _serviceAgendamento;
        private readonly IServiceVeiculos _serviceVeiculos;

        #region Commands
        public ICommand SalvarCommand { get; set; }
        public ICommand LimparCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion

        #region Listas
        public ObservableCollection<Veiculos> Veiculos { get; } = new();
        #endregion

        #region Fields
        private Veiculos? _veiculoSelecionado;
        public Veiculos? VeiculoSelecionado
        {
            get => _veiculoSelecionado;
            set { _veiculoSelecionado = value; OnPropertyChanged(nameof(VeiculoSelecionado)); }
        }

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

            // async void aceitável aqui — é chamada de inicialização, não um command
            _ = CarregarVeiculosAsync();
        }

        private async Task CarregarVeiculosAsync()
        {
            var lista = await _serviceVeiculos.ListarVeiculosAsync();
            Veiculos.Clear();
            foreach (var v in lista)
                Veiculos.Add(v);
        }

        private async Task SalvarAsync()
        {
            try
            {
                MensagemErro = string.Empty;

                DateTime? horarioParsed = null;
                if (!string.IsNullOrWhiteSpace(Horario) &&
                    TimeSpan.TryParse(Horario, out var ts))
                {
                    horarioParsed = DateTime.Today.Add(ts);
                }

                var entity = new AgendamentoManutencao
                {
                    VeiculoId = VeiculoSelecionado?.Id ?? 0,
                    DataAgendamento = DataAgendamento,
                    HorarioAgendamento = horarioParsed,
                    Servico = Servico,
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
            VeiculoSelecionado = null;
            DataAgendamento = DateTime.Today;
            Horario = string.Empty;
            Servico = string.Empty;
            Observacoes = string.Empty;
            MensagemErro = string.Empty;
        }

        private void Cancelar() => CancelamentoSolicitado?.Invoke();
    }
}