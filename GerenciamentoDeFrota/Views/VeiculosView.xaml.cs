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
            var service = CriarService();
            var window = new CadastroVeiculoWindow(service);
            window.ShowDialog();
            await _viewModel.CarregarListaAsync();
        }

        private async void AbrirEdicaoVeiculo(Veiculos veiculo)
        {
            var service = CriarService();
            var window = new CadastroVeiculoWindow(service, veiculo);
            window.ShowDialog();
            await _viewModel.CarregarListaAsync();
        }


        private static ServiceVeiculos CriarService()
        {
            var context = new AppDbContext();
            var repository = new VeiculosRepository(context);
            return new ServiceVeiculos(repository);
        }
    }
}