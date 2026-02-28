using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GerenciamentoDeFrota.Helpers;

namespace GerenciamentoDeFrota.Views
{
    /// <summary>
    /// Interaction logic for VeiculosView.xaml
    /// </summary>
    public partial class VeiculosView : UserControl
    {
        public VeiculosView()
        {
            InitializeComponent();
            AplicarMasacaras();
        }

        private void AplicarMasacaras() 
        {
            TxtPlaca.TextChanged += InputMasks.PlacaMascara_TextChanged;
            TxtAnoModelo.PreviewTextInput += InputMasks.LimitarAno_PreviewTextInput;
            TxtAnoFabricacao.PreviewTextInput += InputMasks.LimitarAno_PreviewTextInput;
            TxtMesEmplacamento.PreviewTextInput += InputMasks.LimitarMesEmplacamento_PreviewTextInput;
            TxtRenavam.PreviewTextInput += InputMasks.LimitarCaracteresNumericos_PreviewTextInput;
        }
    }
}
