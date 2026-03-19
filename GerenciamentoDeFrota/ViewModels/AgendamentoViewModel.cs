using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        private readonly IServiceAgendamento _serviceAgendamento;
        private readonly IServiceVeiculos _serviceVeiculos;

        #region Commands
        public ICommand MesAnteriorCommand { get; set; }
        public ICommand ProximoMesCommand { get; set; }
        public ICommand SelecionarDiaCommand { get; set; }
        public ICommand NovoAgendamentoCommand { get; set; }
        #endregion

        #region Calendário
        public ObservableCollection<DiaCalendario> DiasDoMes { get; } = new();

        private DateTime _mesAtual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        private string _mesAnoAtual = string.Empty;
        public string MesAnoAtual
        {
            get => _mesAnoAtual;
            set { _mesAnoAtual = value; OnPropertyChanged(nameof(MesAnoAtual)); }
        }

        private DateTime _dataSelecionada = DateTime.Today;
        public DateTime DataSelecionada
        {
            get => _dataSelecionada;
            set { _dataSelecionada = value; OnPropertyChanged(nameof(DataSelecionada)); }
        }

        private string _dataSelecionadaLabel = string.Empty;
        public string DataSelecionadaLabel
        {
            get => _dataSelecionadaLabel;
            set { _dataSelecionadaLabel = value; OnPropertyChanged(nameof(DataSelecionadaLabel)); }
        }

        private int _totalAgendamentosDia;
        public int TotalAgendamentosDia
        {
            get => _totalAgendamentosDia;
            set { _totalAgendamentosDia = value; OnPropertyChanged(nameof(TotalAgendamentosDia)); }
        }
        #endregion

        #region Timeline
        public ObservableCollection<AgendamentoSlot> Slots { get; } = new();
        #endregion

        public event Action? AbrirAgendamentoRequested;

        public AgendamentoViewModel(
            IServiceAgendamento serviceAgendamento,
            IServiceVeiculos serviceVeiculos) : base()
        {
            _serviceAgendamento = serviceAgendamento ?? throw new ArgumentNullException(nameof(serviceAgendamento));
            _serviceVeiculos = serviceVeiculos ?? throw new ArgumentNullException(nameof(serviceVeiculos));

            MesAnteriorCommand = new SimpleRelayCommand(async () => await IrMesAnteriorAsync());
            ProximoMesCommand = new SimpleRelayCommand(async () => await IrProximoMesAsync());
            SelecionarDiaCommand = new RelayCommands<DiaCalendario>(async d => await SelecionarDiaAsync(d));
            NovoAgendamentoCommand = new SimpleRelayCommand(AbrirAgendamento);

            _ = CarregarDadosAsync();
        }

        #region Métodos públicos
        public async Task CarregarDadosAsync()
        {
            await GerarGradeMensalAsync();
            await CarregarSlotsAsync();
        }
        #endregion

        #region Navegação de mês
        private async Task IrMesAnteriorAsync()
        {
            _mesAtual = _mesAtual.AddMonths(-1);
            await GerarGradeMensalAsync();
            await CarregarSlotsAsync();
        }

        private async Task IrProximoMesAsync()
        {
            _mesAtual = _mesAtual.AddMonths(1);
            await GerarGradeMensalAsync();
            await CarregarSlotsAsync();
        }
        #endregion

        #region Calendário
        private async Task GerarGradeMensalAsync()
        {
            MesAnoAtual = _mesAtual.ToString("MMMM yyyy").ToUpper();

            var agendamentos = await _serviceAgendamento.ListarAgendamentosAsync();
            var diasComAgendamento = agendamentos
                .Where(a => a.DataAgendamento.HasValue)
                .Select(a => a.DataAgendamento!.Value.Date)
                .ToHashSet();

            DiasDoMes.Clear();

            int primeiroDiaSemana = (int)_mesAtual.DayOfWeek;
            int diasNoMes = DateTime.DaysInMonth(_mesAtual.Year, _mesAtual.Month);

            var mesAnterior = _mesAtual.AddMonths(-1);
            int diasMesAnterior = DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month);

            for (int i = primeiroDiaSemana - 1; i >= 0; i--)
            {
                var data = new DateTime(mesAnterior.Year, mesAnterior.Month, diasMesAnterior - i);
                DiasDoMes.Add(CriarDia(data, false, diasComAgendamento));
            }

            for (int d = 1; d <= diasNoMes; d++)
            {
                var data = new DateTime(_mesAtual.Year, _mesAtual.Month, d);
                DiasDoMes.Add(CriarDia(data, true, diasComAgendamento));
            }

            var mesSeguinte = _mesAtual.AddMonths(1);
            int diaExtra = 1;
            while (DiasDoMes.Count < 42)
            {
                var data = new DateTime(mesSeguinte.Year, mesSeguinte.Month, diaExtra++);
                DiasDoMes.Add(CriarDia(data, false, diasComAgendamento));
            }
        }

        private DiaCalendario CriarDia(DateTime data, bool doMesAtual, HashSet<DateTime> diasComAgendamento)
        {
            bool ehHoje = data.Date == DateTime.Today;
            bool ehSelecionado = data.Date == _dataSelecionada.Date;
            bool temAgendamento = diasComAgendamento.Contains(data.Date);

            return new DiaCalendario
            {
                Data = data,
                Numero = data.Day,
                DoMesAtual = doMesAtual,
                EhHoje = ehHoje,
                EhSelecionado = ehSelecionado,
                TemAgendamentos = temAgendamento,
                FundoCirculo = ehSelecionado ? "#2563EB" : ehHoje ? "#16A34A" : "Transparent",
                TextoNumero = ehSelecionado || ehHoje ? "#FFFFFF" : doMesAtual ? "#1F2A37" : "#C0C0C0",
                OpacidadeCelula = doMesAtual ? 1.0 : 0.4
            };
        }

        private async Task SelecionarDiaAsync(DiaCalendario dia)
        {
            _dataSelecionada = dia.Data;
            DataSelecionadaLabel = dia.Data.ToString("dddd, dd 'de' MMMM",
                new System.Globalization.CultureInfo("pt-BR"));
            await GerarGradeMensalAsync();
            await CarregarSlotsAsync();
        }
        #endregion

        #region Timeline
        private async Task CarregarSlotsAsync()
        {
            Slots.Clear();

            var agendamentosDia = await _serviceAgendamento.ListarPorDataAsync(_dataSelecionada.Date);

            for (int hora = 1; hora <= 23; hora++)
            {
                var slot = new AgendamentoSlot
                {
                    HoraLabel = $"{hora:D2}:00",
                    Agendamentos = agendamentosDia
                        .Where(a => a.HorarioAgendamento.HasValue &&
                                    a.HorarioAgendamento.Value.Hour == hora)
                        .Select(a => new AgendamentoManutencaoDisplay
                        {
                            HoraFormatada = a.HoraFormatada,
                            Veiculo = a.VeiculoDescricao,
                            TipoServico = a.Servico ?? string.Empty,
                            Fornecedor = string.Empty,
                            Responsavel = string.Empty,
                            Cor = "#2563EB"
                        })
                        .ToList()
                };

                Slots.Add(slot);
            }

            TotalAgendamentosDia = agendamentosDia.Count;
            DataSelecionadaLabel = _dataSelecionada.ToString("dddd, dd 'de' MMMM",
                new System.Globalization.CultureInfo("pt-BR"));
        }
        #endregion

        private void AbrirAgendamento() => AbrirAgendamentoRequested?.Invoke();
    }
}