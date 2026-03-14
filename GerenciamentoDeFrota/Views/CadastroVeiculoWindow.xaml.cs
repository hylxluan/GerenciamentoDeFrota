using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Helpers;
using GerenciamentoDeFrota.Interfaces.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace GerenciamentoDeFrota.Views
{
    public partial class CadastroVeiculoWindow : Window
    {
        public CadastroVeiculoWindow(IServiceVeiculos serviceVeiculos, Veiculos? veiculoEditando = null)
        {
            InitializeComponent();
            CadastroVeiculoViewModel viewModel = new CadastroVeiculoViewModel(serviceVeiculos, veiculoEditando);
            viewModel.SalvoComSucesso += () =>
            {
                MessageBox.Show("Veículo salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                WindowHandler.Fechar(this);
            };
            viewModel.CancelamentoSolicitado += () => WindowHandler.Fechar(this);

            DataContext = viewModel;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e) =>
            WindowHandler.Fechar(this);

        private void TxtPlaca_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
            InputMasks.PlacaMascara_TextChanged(sender, e);

        private void TxtRenavam_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarCaracteresNumericos_PreviewTextInput(sender, e);

        private void TxtAnoModelo_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarAno_PreviewTextInput(sender, e);

        private void TxtAnoFabricacao_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarAno_PreviewTextInput(sender, e);

        private void TxtMesEmplacamento_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarMesEmplacamento_PreviewTextInput(sender, e);

        private void TxtKmAtual_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarCaracteresNumericos_KmAtual_PreviewTextInput(sender, e);

        private void TxtKmAtual_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
            InputMasks.KmAtual_TextChanged(sender, e);
    }
}