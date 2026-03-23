using System.Configuration;
using System.Data;
using System.Windows;

namespace GerenciamentoDeFrota
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += (s, ex) =>
            {
                MessageBox.Show(
                    ex.Exception?.InnerException?.Message ?? ex.Exception?.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                ex.Handled = true;
            };

            base.OnStartup(e);
        }


    }

}
