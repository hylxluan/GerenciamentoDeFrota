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
        // ── Guards ───────────────────────────────────────────────────────────
        private static bool _atualizandoMoeda = false;
        private static bool _atualizandoKm = false;

        // ── Placa ────────────────────────────────────────────────────────────
        public static void PlacaMascara_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox placa) return;

            int posicaoInicial = placa.SelectionStart;

            string placaFormatada = new string(placa.Text.ToUpper()
                .Where(char.IsLetterOrDigit).ToArray());

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

        // ── Renavam ──────────────────────────────────────────────────────────
        public static void LimitarCaracteresNumericos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox renavam) return;
            e.Handled = renavam.Text.Length >= 11 || !int.TryParse(e.Text, out _);
        }

        // ── KM Atual ─────────────────────────────────────────────────────────
        public static void LimitarCaracteresNumericos_KmAtual_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txt) return;
            e.Handled = !int.TryParse(e.Text, out _);
        }

        public static void KmAtual_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_atualizandoKm) return;
            if (sender is not TextBox txt) return;

            _atualizandoKm = true;
            try
            {
                string digits = new([.. txt.Text.Where(char.IsDigit)]);

                if (digits.Length == 0)
                {
                    txt.Text = string.Empty;
                    txt.SelectionStart = 0;
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
            finally
            {
                _atualizandoKm = false;
            }
        }

        // ── Ano (4 dígitos, dentro de [anoAtual-60 .. anoAtual]) ─────────────
        public static void LimitarAno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txtAno)
            {
                e.Handled = true;
                return;
            }

            if (!int.TryParse(e.Text, out _) || txtAno.Text.Length >= 4)
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

        // ── Mês de emplacamento (01-12) ───────────────────────────────────────
        public static void LimitarMesEmplacamento_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txt)
            {
                e.Handled = true;
                return;
            }

            string newText = txt.Text + e.Text;

            if (!int.TryParse(newText, out int mes) || mes < 1 || mes > 12)
                e.Handled = true;
        }

        // ── Decimal simples (legado — mantido para compatibilidade) ───────────
        public static void LimitarDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txt) return;
            var c = e.Text[0];
            e.Handled = !char.IsDigit(c) && !(c == ',' && !txt.Text.Contains(','));
        }

        // ── Moeda pt-BR  (ex.: 1.234,56) ─────────────────────────────────────
        /// <summary>
        /// Permite apenas dígitos; a formatação fica por conta do TextChanged.
        /// </summary>
        public static void LimitarMoeda_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        /// <summary>
        /// Formata como decimal pt-BR enquanto o usuário digita.
        /// Os dois últimos dígitos digitados são tratados como centavos (estilo calculadora).
        /// <param name="exibirSimbolo">true → "R$ 1.234,56" | false → "1.234,56"</param>
        /// </summary>
        public static void Moeda_TextChanged(object sender, TextChangedEventArgs e, bool exibirSimbolo = false)
        {
            if (_atualizandoMoeda) return;
            if (sender is not TextBox tb) return;

            _atualizandoMoeda = true;
            try
            {
                string digits = new([.. tb.Text.Where(char.IsDigit)]);

                if (digits.Length == 0)
                {
                    tb.Text = string.Empty;
                    tb.CaretIndex = 0;
                    return;
                }

                decimal valor = decimal.Parse(digits) / 100m;
                string numero = valor.ToString("N2", new CultureInfo("pt-BR"));
                string formatted = exibirSimbolo ? "R$ " + numero : numero;

                if (tb.Text != formatted)
                {
                    tb.Text = formatted;
                    tb.CaretIndex = formatted.Length;
                }
            }
            finally
            {
                _atualizandoMoeda = false;
            }
        }

        // ── CPF: 000.000.000-00 ───────────────────────────────────────────────
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

        // ── CNPJ: 00.000.000/0000-00 ─────────────────────────────────────────
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

        // ── DatePicker ────────────────────────────────────────────────────────
        public static void Data_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var c = e.Text[0];
            e.Handled = !char.IsDigit(c) && c != '/';
        }

        // ── Helpers privados ──────────────────────────────────────────────────
        private static void AtualizarTexto(TextBox txt, string valor)
        {
            if (txt.Text == valor) return;
            int caret = txt.CaretIndex;
            txt.Text = valor;
            txt.CaretIndex = Math.Min(caret + 1, valor.Length);
        }
    }
}