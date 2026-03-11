using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Models
{
    [Serializable]
    public class Veiculos
    {
        public long Id { get; set; }
        public string? Fabricante { get; set; } = string.Empty;
        public string? Modelo { get; set; } = string.Empty;
        public int? AnoModelo { get; set; }
        public int? AnoFabricacao { get; set; }
        public bool? Ativo { get; set; } = true;
        public string? Renavam { get; set; } = string.Empty;
        public string? Placa { get; set; } = string.Empty;
        public int? MesEmplacamento { get; set; }
        public int? AnoEmplacamento { get; set; }
        public DateTime? DataTacografo { get; set; } = DateTime.UtcNow.Date;
        public string? Cor { get; set; } = string.Empty;
        public string? Observacoes { get; set; } = string.Empty;
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow.Date;
        public string VeiculoDescricao => $"{Placa} — {Modelo}";

    }
}