using GerenciamentoDeFrota.Configs;
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
        }

        // async void é correto aqui — é um event handler
        private async void AbrirCadastroVeiculo()
        {
            var context = new AppDbContext();
            var repository = new VeiculosRepository(context);
            var service = new ServiceVeiculos(repository);

            var window = new CadastroVeiculoWindow(service);
            window.ShowDialog();

            await _viewModel.CarregarListaAsync();
        }
    }
}