// ─── VeiculosViewModel.cs ────────────────────────────────────────────────────
using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using GerenciamentoDeFrota.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public class VeiculosViewModel : BaseViewModel
    {
        #region Service
        private readonly IServiceVeiculos _service;
        #endregion

        #region Commands
        public ICommand NovoVeiculoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand DeletarCommand { get; set; }

        // ── Comandos de linha (recebem o item diretamente do DataGrid) ────────
        public ICommand EditarItemCommand { get; set; }
        public ICommand DeletarItemCommand { get; set; }
        #endregion

        #region Listagem e Seleção
        public ObservableCollection<Veiculos> Veiculos { get; } = new();
        private List<Veiculos> _todosVeiculos = new();

        private Veiculos? _selecionado;
        public Veiculos? Selecionado
        {
            get => _selecionado;
            set { _selecionado = value; OnPropertyChanged(nameof(Selecionado)); }
        }
        #endregion

        #region Filtro
        private string _filtro = string.Empty;
        public string Filtro
        {
            get => _filtro;
            set
            {
                _filtro = value;
                OnPropertyChanged(nameof(Filtro));
                AplicarFiltro();
            }
        }
        #endregion

        #region Mensagens
        private string _mensagemErro = string.Empty;
        public string MensagemErro
        {
            get => _mensagemErro;
            set { _mensagemErro = value; OnPropertyChanged(nameof(MensagemErro)); }
        }

        private string _mensagemSucesso = string.Empty;
        public string MensagemSucesso
        {
            get => _mensagemSucesso;
            set { _mensagemSucesso = value; OnPropertyChanged(nameof(MensagemSucesso)); }
        }
        #endregion

        #region Eventos para o code-behind
        public event Action? AbrirCadastroRequested;
        public event Action<Veiculos>? EditarRequested;
        #endregion

        public VeiculosViewModel(IServiceVeiculos service) : base()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

            NovoVeiculoCommand = new SimpleRelayCommand(AbrirCadastro);
            EditarCommand = new SimpleRelayCommand(Editar);
            DeletarCommand = new SimpleRelayCommand(async () => await DeletarAsync());

            // Comandos de linha — recebem o Veiculos como parâmetro
            EditarItemCommand = new RelayCommands<Veiculos>(v =>
            {
                if (v is null) return;
                Selecionado = v;
                LimparMensagens();
                EditarRequested?.Invoke(v);
            });

            DeletarItemCommand = new RelayCommands<Veiculos>(async v =>
            {
                if (v is null) return;
                Selecionado = v;
                await DeletarAsync();
            });

            _ = CarregarListaAsync();
        }

        #region Ações
        private void AbrirCadastro() => AbrirCadastroRequested?.Invoke();

        private void Editar()
        {
            if (Selecionado is null)
            {
                MensagemErro = "Selecione um veículo para editar.";
                return;
            }
            LimparMensagens();
            EditarRequested?.Invoke(Selecionado);
        }

        private async Task DeletarAsync()
        {
            try
            {
                LimparMensagens();

                if (Selecionado is null)
                {
                    MensagemErro = "Selecione um veículo para deletar.";
                    return;
                }

                var confirmar = MessageBox.Show(
                    $"Tem certeza que deseja excluir o veículo \"{Selecionado.Modelo} — {Selecionado.Placa}\"?\nEssa ação não pode ser desfeita.",
                    "Confirmar Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirmar is not MessageBoxResult.Yes) return;

                await _service.DeletarVeiculoAsync(Selecionado.Id);
                await CarregarListaAsync();
                Selecionado = null;
                MensagemSucesso = "Veículo removido com sucesso!";
            }
            catch (VeiculoPossuiVinculosException ex)
            {
                var confirmarCascata = MessageBox.Show(
                    $"{ex.Message}\n\nDeseja excluir o veículo junto com todos os registros vinculados?\n\nEssa ação é irreversível.",
                    "Veículo com vínculos",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirmarCascata is not MessageBoxResult.Yes) return;

                try
                {
                    await _service.DeletarVeiculoComVinculosAsync(Selecionado!.Id);
                    await CarregarListaAsync();
                    Selecionado = null;
                    MensagemSucesso = "Veículo e registros vinculados removidos com sucesso!";
                }
                catch (Exception innerEx)
                {
                    MensagemErro = $"Erro ao excluir: {innerEx.Message}";
                }
            }
            catch (GerenciamentoDeFrotaExceptions ex)
            {
                MensagemErro = ex.Message;
            }
            catch (Exception)
            {
                MensagemErro = "Erro inesperado ao deletar. Contate o suporte.";
            }
        }
        #endregion

        #region Métodos auxiliares
        public async Task CarregarListaAsync()
        {
            // Usa o método que inclui CentrosCusto para exibir o nome no grid
            _todosVeiculos = await _service.ListarVeiculosComCentroAsync();
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            Veiculos.Clear();

            var lista = string.IsNullOrWhiteSpace(Filtro)
                ? _todosVeiculos
                : _todosVeiculos.Where(v =>
                      (v.Placa?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (v.Modelo?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (v.Fabricante?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (v.NumeroFrota?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false))
                  .ToList();

            foreach (var item in lista)
                Veiculos.Add(item);
        }

        private void LimparMensagens()
        {
            MensagemErro = string.Empty;
            MensagemSucesso = string.Empty;
        }
        #endregion
    }
}