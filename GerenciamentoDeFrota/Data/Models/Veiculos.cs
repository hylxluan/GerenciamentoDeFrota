using GerenciamentoDeFrota.Enums;

namespace GerenciamentoDeFrota.Data.Models
{
    [Serializable]
    public class Veiculos
    {
        // Identificação
        public long Id { get; set; }
        public string? Fabricante { get; set; } = string.Empty;
        public string? Modelo { get; set; } = string.Empty;
        public string? Cor { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty;
        public DateTime? DataTacografo { get; set; } = DateTime.UtcNow.Date;
        public string? Placa { get; set; } = string.Empty;
        public string? Renavam { get; set; } = string.Empty;
        public string? NumeroFrota { get; set; } = string.Empty;
        public string? UF { get; set; } = string.Empty;
        public int? AnoFabricacao { get; set; }
        public int? AnoModelo { get; set; }
        public int? MesEmplacamento { get; set; }
        public int? AnoEmplacamento { get; set; }
        public int? KmAtual { get; set; }
        public bool? Ativo { get; set; } = true;
        public bool? Terceirizado { get; set; } = false;
        public bool? VeiculoTracao { get; set; } = true;
        public EquipamentoUtiliza KmHora { get; set; } = EquipamentoUtiliza.Km;
        public string? Observacoes { get; set; } = string.Empty;
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow.Date;

        // Proprietário
        public string? Proprietario { get; set; } = string.Empty;
        public string? CPF { get; set; } = string.Empty;
        public string? CNPJ { get; set; } = string.Empty;

        // Dados Técnicos
        public decimal? ValorFipe { get; set; }
        public decimal? CapacidadeTanque { get; set; }
        public string? Padronizacao { get; set; } = string.Empty;
        public string? Carroceria { get; set; } = string.Empty;
        public decimal? CapacidadePaletes { get; set; }   // M³ / Litros / Kg
        public decimal? CapacidadeCaixa { get; set; }
        public decimal? TaraKg { get; set; }
        public decimal? LotacaoKg { get; set; } 

        // Licenciamento
        public int? Licenciamento { get; set; }
        public DateTime? LicenciamentoDtVencimento { get; set; }

        // IPVA
        public int? Ipva { get; set; } 
        public DateTime? IpvaDtVencimento { get; set; }

        // ANTT
        public string? Antt { get; set; } = string.Empty;
        public DateTime? AnttDtVencimento { get; set; }

        // Certificado Cronoacógrafo
        public DateTime? CronoacografoDtVencimento { get; set; }

        // Extintor de Incêndio
        public DateTime? ExtintorDtVencimento { get; set; }
        public string? ExtintorCodigo { get; set; } = string.Empty;

        // Seguro do Veículo
        public string? SeguroSeguradora { get; set; } = string.Empty;
        public string? SeguroNrApolice { get; set; } = string.Empty;
        public DateTime? SeguroDtInicioVigencia { get; set; }
        public DateTime? SeguroDtTerminoVigencia { get; set; }
        public string? SeguroTipo { get; set; } = string.Empty;

        // Navegação
        public ICollection<VeiculoDocumento> Documentos { get; set; } = [];
        public ICollection<VeiculoAnexo> Anexos { get; set; } = [];

        // Computado
        public string VeiculoDescricao => $"{Placa} — {Modelo}";

        public long? CentrosCustoId { get; set; }
        public CentrosCusto? CentrosCusto { get; set; }
    }
}