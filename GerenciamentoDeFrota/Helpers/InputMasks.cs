using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static void LimitarCaracteresNumericos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox renavam) return;

            e.Handled = renavam.Text.Length >= 11 || !int.TryParse(e.Text, out _);
        }

        public static void PlacaMascara_TextChanged(object sender, TextChangedEventArgs e) 
        { 
            if (sender is not TextBox placa) return;

            int posicaoInicial = placa.SelectionStart;

            string placaFormatada = new string(placa.Text.ToUpper().Where(character => char.IsLetterOrDigit(character)).ToArray());

            if (placaFormatada.Length > 7) placaFormatada = placaFormatada.Substring(0, 7);


            var result = "";


            for (int i = 0; i < placaFormatada.Length; i++)
            {
                char c = placaFormatada[i];

                switch (i) 
                {
                    case 0:
                    case 1:
                    case 2:
                        if (char.IsLetter(c)) result += c;
                        break;
                    case 3:
                            if (char.IsDigit(c)) result += c;
                            break;
                    case 4:
                        if (char.IsLetter(c)) result += c;
                        break;
                    case 5:
                    case 6:
                        if (char.IsDigit(c)) result += c;
                        break;
                }

            }
            if (placa.Text != result)
            {
                placa.Text = result;
                placa.SelectionStart = posicaoInicial > placa.Text.Length ? placa.Text.Length : posicaoInicial;
            }
        }

        public static void LimitarAno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox txtAno)
            {
                if (!int.TryParse(e.Text, out _))
                {
                    e.Handled = true;
                    return;
                }

                if (txtAno.Text.Length >= 4)
                {
                    e.Handled = true;
                    return;
                }

                string newText = txtAno.Text + e.Text;
                if (newText.Length == 4 && int.TryParse(newText, out int ano))
                {
                    int anoAtual = DateTime.Now.Year;
                    int anoMinimo = anoAtual - 60;
                    e.Handled = ano > anoAtual || ano < anoMinimo;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
