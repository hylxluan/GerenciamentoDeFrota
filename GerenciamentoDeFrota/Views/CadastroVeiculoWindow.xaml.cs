using GerenciamentoDeFrota.Helpers;
using GerenciamentoDeFrota.Interfaces.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GerenciamentoDeFrota.Views
{
    public partial class CadastroVeiculoWindow : Window
    {
        public CadastroVeiculoWindow(IServiceVeiculos service)
        {
            InitializeComponent();

            var viewModel = new CadastroVeiculoViewModel(service);
            viewModel.SalvoComSucesso += () => this.Close();
            viewModel.CancelamentoSolicitado += () => this.Close();

            DataContext = viewModel;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
            => WindowHandler.Fechar(this);

        // Máscaras
        private void TxtPlaca_TextChanged(object sender, TextChangedEventArgs e)
            => InputMasks.PlacaMascara_TextChanged(sender, e);

        private void TxtRenavam_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => InputMasks.LimitarCaracteresNumericos_PreviewTextInput(sender, e);

        private void TxtAnoModelo_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => InputMasks.LimitarAno_PreviewTextInput(sender, e);

        private void TxtAnoFabricacao_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => InputMasks.LimitarAno_PreviewTextInput(sender, e);

        private void TxtMesEmplacamento_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => InputMasks.LimitarMesEmplacamento_PreviewTextInput(sender, e);
    }
}