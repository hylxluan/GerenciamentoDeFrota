using System.Windows;
using System.Windows.Input;
using GerenciamentoDeFrota.Configs;
using GerenciamentoDeFrota.Data.Repositories;
using GerenciamentoDeFrota.Data.Services;
using GerenciamentoDeFrota.Views;
using GerenciamentoDeFrota.ViewModels;
using GerenciamentoDeFrota.Helpers;

namespace GerenciamentoDeFrota
{
    public partial class MainWindow : Window
    {
        #region Cache de Views
        private DashboardView? _dashboardView;
        private VeiculosView? _veiculosView;
        private CondutoresView? _condutoresView;
        private CentrosCustoView? _centrosCustoView;
        private FornecedoresView? _fornecedoresView;
        private CombustivelView? _combustivelView;
        private VeiculoCombustivelView? _veiculoCombustivelView;
        private AgendamentoView? _agendamentoView;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Arrastar janela
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                WindowHandler.Maximizar(this);
        }
        #endregion

        #region Controles da janela
        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
            => WindowHandler.Minimmizar(this);

        private void BtnMaximizar_Click(object sender, RoutedEventArgs e)
            => WindowHandler.Maximizar(this);

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
            => WindowHandler.Fechar(this);
        #endregion

        #region Navegação
        private void RbDashboard_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _dashboardView ??= new DashboardView();
            MainContentHost.Content = _dashboardView;
        }

        private void RbVeiculos_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _veiculosView ??= new VeiculosView();
            MainContentHost.Content = _veiculosView;
        }

        private void RbCondutores_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _condutoresView ??= new CondutoresView();
            MainContentHost.Content = _condutoresView;
        }

        private void RbCentrosCusto_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;

            if (_centrosCustoView == null)
            {
                var context = new AppDbContext();
                var repository = new CentrosCustoRepository(context);
                var services = new ServiceCentrosCusto(repository);

                _centrosCustoView = new CentrosCustoView
                {
                    DataContext = new CentrosCustoViewModel(services)
                };
            }

            MainContentHost.Content = _centrosCustoView;
        }

        private void RbFornecedores_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _fornecedoresView ??= new FornecedoresView();
            MainContentHost.Content = _fornecedoresView;
        }

        private void RbCombustivel_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _combustivelView ??= new CombustivelView();
            MainContentHost.Content = _combustivelView;
        }

        private void RbVeiculoCombustivel_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _veiculoCombustivelView ??= new VeiculoCombustivelView();
            MainContentHost.Content = _veiculoCombustivelView;
        }

        private void RbAgendamento_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            _agendamentoView ??= new AgendamentoView();
            MainContentHost.Content = _agendamentoView;
        }
        #endregion
    }
}