using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Repositories;
using GerenciamentoDeFrota.Data.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows.Controls;

namespace GerenciamentoDeFrota.Views
{
    public partial class AgendamentoView : UserControl
    {
        private AgendamentoViewModel _viewModel;

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

            // Assina o evento do ViewModel para abrir a window modal
            _viewModel.AbrirAgendamentoRequested += AbrirCadastroAgendamento;
        }

        private void AbrirCadastroAgendamento()
        {
            var context = new AppDbContext();
            var agendamentoRepository = new AgendamentoRepository(context);
            var veiculosRepository = new VeiculosRepository(context);
            var serviceAgendamento = new ServiceAgendamento(agendamentoRepository);
            var serviceVeiculos = new ServiceVeiculos(veiculosRepository);

            var window = new CadastroAgendamentoWindow(serviceAgendamento, serviceVeiculos);

            // ShowDialog() bloqueia a MainWindow até fechar
            window.ShowDialog();

            // Após fechar, recarrega calendário e slots do dia
            _viewModel.CarregarDados();
        }
    }
}