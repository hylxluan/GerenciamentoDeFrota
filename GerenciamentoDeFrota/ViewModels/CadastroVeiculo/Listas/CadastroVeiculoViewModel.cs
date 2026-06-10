// ─── CadastroVeiculoViewModel.Listas.cs ──────────────────────────────────────
// Listas estáticas usadas nos ComboBoxes do formulário.
// São readonly e instanciadas uma única vez — não geram overhead por abertura.
// ─────────────────────────────────────────────────────────────────────────────
namespace GerenciamentoDeFrota.ViewModels
{
    public partial class CadastroVeiculoViewModel
    {
        public List<string> TiposVeiculo { get; } =
        [
            "Automóvel / Carro de Passeio", "Caminhonete (Pick-up)", "SUV / Utilitário Esportivo",
            "Van / Minivan", "Furgão", "Motocicleta / Moto", "Triciclo Motorizado", "Quadriciclo",
            "Micro-ônibus", "Ônibus Urbano", "Ônibus Rodoviário", "Caminhão Leve (até 7,5t)",
            "Caminhão Médio (7,5t a 16t)", "Caminhão Pesado (16t a 40t)",
            "Caminhão Extrapesado (acima de 40t)", "Cavalo Mecânico / Caminhão-Trator",
            "Caminhão Basculante", "Caminhão Betoneira", "Caminhão Tanque", "Caminhão Frigorífico",
            "Caminhão Cegonha", "Caminhão Plataforma / Prancha", "Caminhão Guincho",
            "Trator Agrícola", "Colheitadeira / Combinada", "Pulverizador Autopropelido",
            "Plantadeira", "Trator de Esteira", "Escavadeira Hidráulica", "Retroescavadeira",
            "Pá Carregadeira (Loader)", "Motoniveladora (Patrol)", "Rolo Compactador",
            "Mini Carregadeira (Bobcat / Skid Steer)", "Guindaste / Munck", "Empilhadeira",
            "Perfuratriz / Sonda", "Ambulância", "Viatura Policial", "Caminhão de Bombeiros",
            "Veículo Blindado", "Reboque", "Semirreboque / Carreta", "Outros"
        ];

        public List<string> TiposKmHora { get; } = ["Km", "Horímetro"];

        public List<string> EstadosBrasil { get; } =
        [
            "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO",
            "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR",
            "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO"
        ];
    }
}