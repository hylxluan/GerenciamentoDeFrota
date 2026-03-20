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
        private string _filtroPlaca = string.Empty;
        public string FiltroPlaca
        {
            get => _filtroPlaca;
            set
            {
                _filtroPlaca = value;
                OnPropertyChanged(nameof(FiltroPlaca));
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

                // 1ª confirmação
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
                // 2ª confirmação — tem vínculos, pergunta se quer tudo
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
            _todosVeiculos = await _service.ListarVeiculosAsync();
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            Veiculos.Clear();

            var lista = string.IsNullOrWhiteSpace(FiltroPlaca)
                ? _todosVeiculos
                : _todosVeiculos
                    .Where(v => v.Placa is not null &&
                                v.Placa.Contains(FiltroPlaca, StringComparison.OrdinalIgnoreCase))
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