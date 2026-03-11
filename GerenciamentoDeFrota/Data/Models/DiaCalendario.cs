using System;

namespace GerenciamentoDeFrota.Data.Models
{
    public class DiaCalendario
    {
        public DateTime Data { get; set; }
        public int Numero { get; set; }
        public bool DoMesAtual { get; set; }
        public bool EhHoje { get; set; }
        public bool EhSelecionado { get; set; }
        public bool TemAgendamentos { get; set; }

        // Usados como bindings no XAML da grade do calendário
        public string FundoCirculo { get; set; } = "Transparent";
        public string TextoNumero { get; set; } = "#1F2A37";
        public double OpacidadeCelula { get; set; } = 1.0;
    }
}