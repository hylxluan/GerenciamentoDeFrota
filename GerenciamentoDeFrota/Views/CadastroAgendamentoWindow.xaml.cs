using GerenciamentoDeFrota.Helpers;
using GerenciamentoDeFrota.Interfaces.Services;
using GerenciamentoDeFrota.ViewModels;
using System.Windows;
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

        // Máscara horário: permite apenas dígitos e ":"
        private void TxtHorario_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]) && e.Text[0] != ':';
        }
    }
}