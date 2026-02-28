using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;

namespace GerenciamentoDeFrota.Helpers
{
    public static class InputMasks
    {
       
        public static void LimitarMesEmplacamento_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox mesEmplacamento)
            {
                string newText = mesEmplacamento.Text + e.Text;


                if (int.TryParse(newText, out int mes))
                {
                    e.Handled = mes < 1 || mes > 12;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
