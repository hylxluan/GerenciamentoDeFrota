using System;
using System.Globalization;
using System.Linq;
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
                    if (mes < 1 || mes > 12)
                        goto bloquear;
                }
                else
                {
                    goto bloquear;
                }
            }
            else
            {
                goto bloquear;
            }

            return;
        bloquear:
            e.Handled = true;
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

            string placaFormatada = new string(placa.Text.ToUpper()
                .Where(character => char.IsLetterOrDigit(character)).ToArray());

            if (placaFormatada.Length > 7)
                placaFormatada = placaFormatada[..7];

            string result = string.Empty;

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
                placa.SelectionStart = posicaoInicial > placa.Text.Length
                    ? placa.Text.Length
                    : posicaoInicial;
            }
        }

        public static void LimitarAno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox txtAno)
            {
                if (!int.TryParse(e.Text, out _))
                    goto bloquear;

                if (txtAno.Text.Length >= 4)
                    goto bloquear;

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
                goto bloquear;
            }

            return;
        bloquear:
            e.Handled = true;
        }

        public static void LimitarCaracteresNumericos_KmAtual_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txt) return;
            e.Handled = !int.TryParse(e.Text, out _);
        }

        public static void KmAtual_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox txt) return;

            string digits = new([.. txt.Text.Where(char.IsDigit)]);

            if (digits.Length == 0)
            {
                if (txt.Text != string.Empty)
                {
                    txt.Text = string.Empty;
                    txt.SelectionStart = 0;
                }
                return;
            }

            if (digits.Length > 7)
                digits = digits[..7];

            long valor = long.Parse(digits);
            string formatado = valor.ToString("N0", new CultureInfo("pt-BR"));

            if (txt.Text != formatado)
            {
                txt.Text = formatado;
                txt.SelectionStart = formatado.Length;
            }
        }

        public static void LimitarDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txt) return;

            var c = e.Text[0];
            bool ehDigito = char.IsDigit(c);
            bool ehVirgula = c == ',' && !txt.Text.Contains(',');

            e.Handled = !ehDigito && !ehVirgula;
        }

        // Máscara CPF: 000.000.000-00
        public static void CPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox txt) return;

            string digits = new([.. txt.Text.Where(char.IsDigit).Take(11)]);

            string mask = digits.Length switch
            {
                <= 3 => digits,
                <= 6 => $"{digits[..3]}.{digits[3..]}",
                <= 9 => $"{digits[..3]}.{digits[3..6]}.{digits[6..]}",
                _ => $"{digits[..3]}.{digits[3..6]}.{digits[6..9]}-{digits[9..]}"
            };

            AtualizarTexto(txt, mask);
        }

        // Máscara CNPJ: 00.000.000/0000-00
        public static void CNPJ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox txt) return;

            string digits = new([.. txt.Text.Where(char.IsDigit).Take(14)]);

            string mask = digits.Length switch
            {
                <= 2 => digits,
                <= 5 => $"{digits[..2]}.{digits[2..]}",
                <= 8 => $"{digits[..2]}.{digits[2..5]}.{digits[5..]}",
                <= 12 => $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..]}",
                _ => $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..12]}-{digits[12..]}"
            };

            AtualizarTexto(txt, mask);
        }

        private static void AtualizarTexto(TextBox txt, string valor)
        {
            if (txt.Text == valor) return;
            int caret = txt.CaretIndex;
            txt.Text = valor;
            txt.CaretIndex = Math.Min(caret + 1, valor.Length);
        }

        
        public static void Data_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var c = e.Text[0];
            e.Handled = !char.IsDigit(c) && c != '/';
        }
    }
}