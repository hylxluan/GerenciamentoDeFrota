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

            // ── Carrega o veículo completo (com Documentos, Anexos e CentrosCusto) ──
            // O objeto que vem do DataGrid não tem as navegações populadas
            var veiculoCompleto = await service.ObterVeiculoCompletoAsync(veiculo.Id);

            if (veiculoCompleto is null)
            {
                // Veículo foi removido por outra sessão entre o carregamento e o clique
                await _viewModel.CarregarListaAsync();
                return;
            }

            var window = new CadastroVeiculoWindow(service, serviceCentrosCusto, veiculoCompleto);
            window.ShowDialog();
            await _viewModel.CarregarListaAsync();
        }

        private static (ServiceVeiculos, ServiceCentrosCusto) CriarServices()
        {
            var context = new AppDbContext();
            return (new ServiceVeiculos(new VeiculosRepository(context)),
                    new ServiceCentrosCusto(new CentrosCustoRepository(context)));
        }
    }
}