using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Helpers;
using GerenciamentoDeFrota.Interfaces.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GerenciamentoDeFrota.Views
{
    public partial class CadastroAgendamentoWindow : Window
    {
        public CadastroAgendamentoWindow(
            IServiceAgendamento serviceAgendamento,
            IServiceVeiculos serviceVeiculos)
        {
            InitializeComponent();

            var viewModel = new CadastroAgendamentoViewModel(serviceAgendamento, serviceVeiculos);
            viewModel.SalvoComSucesso += () => WindowHandler.Fechar(this);
            viewModel.CancelamentoSolicitado += () => WindowHandler.Fechar(this);

            DataContext = viewModel;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
            => WindowHandler.Fechar(this);

        // ── Autocomplete — veículo ────────────────────────────────────────────

        private void TxtBuscaVeiculo_GotFocus(object sender, RoutedEventArgs e)
        {
            // Reabre o popup se já houver texto digitado
            if (DataContext is CadastroAgendamentoViewModel vm &&
                vm.VeiculosFiltrados.Count > 0)
                vm.PopupVeiculoAberto = true;
        }

        private void TxtBuscaVeiculo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is not CadastroAgendamentoViewModel vm) return;

            switch (e.Key)
            {
                case Key.Down:
                    // Move o foco para o ListBox
                    if (vm.PopupVeiculoAberto && ListSugestoes.Items.Count > 0)
                    {
                        ListSugestoes.Focus();
                        ListSugestoes.SelectedIndex = 0;
                    }
                    e.Handled = true;
                    break;

                case Key.Escape:
                    vm.PopupVeiculoAberto = false;
                    e.Handled = true;
                    break;

                case Key.Enter:
                    // Seleciona o primeiro item da lista se popup aberto
                    if (vm.PopupVeiculoAberto && vm.VeiculosFiltrados.Count > 0)
                        vm.VeiculoSelecionado = vm.VeiculosFiltrados[0];
                    e.Handled = true;
                    break;
            }
        }

        private void ListSugestoes_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Captura o item clicado antes do Popup fechar
            if (e.OriginalSource is FrameworkElement fe &&
                fe.DataContext is Veiculos veiculo &&
                DataContext is CadastroAgendamentoViewModel vm)
            {
                vm.VeiculoSelecionado = veiculo;
                TxtBuscaVeiculo.Focus();
                e.Handled = true;
            }
        }

        // ── Máscara de horário (HH:mm) ────────────────────────────────────────
        private void TxtHorario_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]) && e.Text[0] != ':';
        }

        // ── Máscara de KM / Horímetro ─────────────────────────────────────────
        private void TxtKmAtualAgendamento_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => InputMasks.LimitarCaracteresNumericos_KmAtual_PreviewTextInput(sender, e);

        private void TxtKmAtualAgendamento_TextChanged(object sender, TextChangedEventArgs e)
            => InputMasks.KmAtual_TextChanged(sender, e);
    }
}