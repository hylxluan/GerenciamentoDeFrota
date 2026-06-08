// ─── CadastroVeiculoWindow.xaml.cs ───────────────────────────────────────────
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Helpers;
using GerenciamentoDeFrota.Interfaces.Services;
using GerenciamentoDeFrota.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace GerenciamentoDeFrota.Views
{
    public partial class CadastroVeiculoWindow : Window
    {
        private readonly IServiceVeiculos _service;
        private readonly CadastroVeiculoViewModel _vm;

        public CadastroVeiculoWindow(
            IServiceVeiculos service,
            IServiceCentrosCusto serviceCentrosCusto,
            Veiculos? veiculoEditando = null)
        {
            InitializeComponent();

            _service = service;
            _vm = new CadastroVeiculoViewModel(service, serviceCentrosCusto, veiculoEditando);

            _vm.SalvoComSucesso += () => WindowHandler.Fechar(this);
            _vm.CancelamentoSolicitado += () => WindowHandler.Fechar(this);
            _vm.DeletarSolicitado += ConfirmarEDeletar;
            _vm.AdicionarAnexoSolicitado += SelecionarAnexo;

            DataContext = _vm;
        }

        // ── Exclusão ─────────────────────────────────────────────────────────
        private async void ConfirmarEDeletar()
        {
            var confirmar = MessageBox.Show(
                "Tem certeza que deseja excluir este veículo?\nEssa ação não pode ser desfeita.",
                "Confirmar Exclusão",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirmar is not MessageBoxResult.Yes) return;

            try
            {
                await _service.DeletarVeiculoAsync(_vm.GetIdEditando());
                WindowHandler.Fechar(this);
            }
            catch (VeiculoPossuiVinculosException ex)
            {
                var confirmarCascata = MessageBox.Show(
                    $"{ex.Message}\n\nDeseja excluir o veículo junto com todos os registros vinculados?\n\nEssa ação é irreversível.",
                    "Veículo com vínculos",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirmarCascata is not MessageBoxResult.Yes) return;

                try
                {
                    await _service.DeletarVeiculoComVinculosAsync(_vm.GetIdEditando());
                    WindowHandler.Fechar(this);
                }
                catch (Exception innerEx)
                {
                    MessageBox.Show($"Erro ao excluir: {innerEx.Message}", "Erro",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ── Anexos ───────────────────────────────────────────────────────────
        private void SelecionarAnexo()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Selecionar Anexo",
                Filter = "Arquivos|*.pdf;*.jpg;*.jpeg;*.png;*.doc;*.docx;*.xls;*.xlsx",
                Multiselect = true
            };

            if (dialog.ShowDialog() is not true) return;

            foreach (var arquivo in dialog.FileNames)
                _vm.AdicionarAnexo(arquivo);
        }

        // ── Drag / Fechar ────────────────────────────────────────────────────
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e) =>
            WindowHandler.Fechar(this);

        // ── Máscaras: existentes ─────────────────────────────────────────────
        private void TxtPlaca_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
            InputMasks.PlacaMascara_TextChanged(sender, e);

        private void TxtRenavam_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarCaracteresNumericos_PreviewTextInput(sender, e);

        private void TxtKmAtual_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarCaracteresNumericos_KmAtual_PreviewTextInput(sender, e);

        private void TxtKmAtual_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
            InputMasks.KmAtual_TextChanged(sender, e);

        // ── Máscaras: novas ──────────────────────────────────────────────────
        private void TxtAno_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarAno_PreviewTextInput(sender, e);

        private void TxtMes_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarMesEmplacamento_PreviewTextInput(sender, e);

        private void TxtDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.LimitarDecimal_PreviewTextInput(sender, e);

        private void TxtCPF_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
            InputMasks.CPF_TextChanged(sender, e);

        private void TxtCNPJ_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
            InputMasks.CNPJ_TextChanged(sender, e);

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            InputMasks.Data_PreviewTextInput(sender, e);
    }
}