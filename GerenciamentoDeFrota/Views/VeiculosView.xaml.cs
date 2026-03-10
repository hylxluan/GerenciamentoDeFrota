using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Repositories;
using GerenciamentoDeFrota.Data.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows.Controls;

namespace GerenciamentoDeFrota.Views
{
    public partial class VeiculosView : UserControl
    {
        private VeiculosViewModel _viewModel;

        public VeiculosView()
        {
            InitializeComponent();

            var context = new AppDbContext();
            var repository = new VeiculosRepository(context);
            var service = new ServiceVeiculos(repository);
            _viewModel = new VeiculosViewModel(service);

            DataContext = _viewModel;

            // Assina o evento do ViewModel para abrir a window modal
            _viewModel.AbrirCadastroRequested += AbrirCadastroVeiculo;
        }

        private void AbrirCadastroVeiculo()
        {
            var context = new AppDbContext();
            var repository = new VeiculosRepository(context);
            var service = new ServiceVeiculos(repository);

            var window = new CadastroVeiculoWindow(service);

            // ShowDialog() bloqueia a MainWindow até fechar
            window.ShowDialog();

            // Após fechar (salvo ou cancelado), recarrega o grid
            _viewModel.CarregarLista();
        }
    }
}