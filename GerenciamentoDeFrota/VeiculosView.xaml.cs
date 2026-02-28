using System.Windows.Controls;
using GerenciamentoDeFrota.Helpers;


namespace GerenciamentoDeFrota.Views
{
    public partial class VeiculosView : UserControl
    {
        public VeiculosView()
        {
            InitializeComponent();
            AplicarMascaras();
        }

        private void AplicarMascaras() 
        {
            TxtMesEmplacamento.PreviewTextInput += InputMasks.LimitarMesEmplacamento_PreviewTextInput; ;


        }

    }
}