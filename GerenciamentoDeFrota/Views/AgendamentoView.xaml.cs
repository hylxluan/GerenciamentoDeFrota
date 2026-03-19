using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Repositories;
using GerenciamentoDeFrota.Data.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows.Controls;

namespace GerenciamentoDeFrota.Views
{
    public partial class AgendamentoView : UserControl
    {
        private readonly AgendamentoViewModel _viewModel;

        public AgendamentoView()
        {
            InitializeComponent();

            var context = new AppDbContext();
            var agendamentoRepository = new AgendamentoRepository(context);
            var veiculosRepository = new VeiculosRepository(context);
            var serviceAgendamento = new ServiceAgendamento(agendamentoRepository);
            var serviceVeiculos = new ServiceVeiculos(veiculosRepository);

            _viewModel = new AgendamentoViewModel(serviceAgendamento, serviceVeiculos);
            DataContext = _viewModel;

            _viewModel.AbrirAgendamentoRequested += AbrirCadastroAgendamento;
        }

        private async void AbrirCadastroAgendamento()
        {
            var context = new AppDbContext();
            var agendamentoRepository = new AgendamentoRepository(context);
            var veiculosRepository = new VeiculosRepository(context);
            var serviceAgendamento = new ServiceAgendamento(agendamentoRepository);
            var serviceVeiculos = new ServiceVeiculos(veiculosRepository);

            var window = new CadastroAgendamentoWindow(serviceAgendamento, serviceVeiculos);
            window.ShowDialog();

            await _viewModel.CarregarDadosAsync();
        }
    }
}