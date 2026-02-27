using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GerenciamentoDeFrota
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = IsMaximized ? WindowState.Normal : WindowState.Maximized;
            }

        }
        private void RbDashboard_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            MainContentHost.Content = new Views.DashboardView();
        }

        private void RbVeiculos_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;

            var view = new Views.VeiculosView();
            MainContentHost.Content = view;      
        }

        private void RbCondutores_Checked(object sender, RoutedEventArgs e)
        {
            if (MainContentHost == null) return;
            MainContentHost.Content = new Views.CondutoresView();
        }
    }
}