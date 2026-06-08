using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Data.Repositories;
using GerenciamentoDeFrota.Data.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows.Controls;

namespace GerenciamentoDeFrota.Views
{
    public partial class VeiculosView : UserControl
    {
        private readonly VeiculosViewModel _viewModel;

        public VeiculosView()
        {
            InitializeComponent();

            var context = new AppDbContext();
            var repository = new VeiculosRepository(context);
            var service = new ServiceVeiculos(repository);
            _viewModel = new VeiculosViewModel(service);

            DataContext = _viewModel;

            _viewModel.AbrirCadastroRequested += AbrirCadastroVeiculo;
            _viewModel.EditarRequested += AbrirEdicaoVeiculo;
        }

        private async void AbrirCadastroVeiculo()
        {
            var (service, serviceCentrosCusto) = CriarServices();
            var window = new CadastroVeiculoWindow(service, serviceCentrosCusto);
            window.ShowDialog();
            await _viewModel.CarregarListaAsync();
        }

        private async void AbrirEdicaoVeiculo(Veiculos veiculo)
        {
            var (service, serviceCentrosCusto) = CriarServices();
            var window = new CadastroVeiculoWindow(service, serviceCentrosCusto, veiculo);
            window.ShowDialog();
            await _viewModel.CarregarListaAsync();
        }

        private static (ServiceVeiculos, ServiceCentrosCusto) CriarServices()
        {
            var context = new AppDbContext();

            var veiculosRepo = new VeiculosRepository(context);
            var centrosRepo = new CentrosCustoRepository(context);

            return (new ServiceVeiculos(veiculosRepo),
                    new ServiceCentrosCusto(centrosRepo));
        }
    }
}