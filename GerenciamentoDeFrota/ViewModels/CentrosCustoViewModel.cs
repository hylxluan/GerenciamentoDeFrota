using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using GerenciamentoDeFrota.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CentrosCustoViewModel : BaseViewModel
    {
        #region Commands
        public ICommand SalvarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand LimparCommand { get; set; }
        public ICommand DeletarCommand { get; set; }
        #endregion

        #region Fields
        private string _nome = string.Empty;
        public string Nome
        {
            get => _nome;
            set { _nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        private string _observacoes = string.Empty;
        public string Observacoes
        {
            get => _observacoes;
            set { _observacoes = value; OnPropertyChanged(nameof(Observacoes)); }
        }

        private bool _ativo = true;
        public bool Ativo
        {
            get => _ativo;
            set { _ativo = value; OnPropertyChanged(nameof(Ativo)); }
        }

        private string _filtroNome = string.Empty;
        public string FiltroNome
        {
            get => _filtroNome;
            set { _filtroNome = value; OnPropertyChanged(nameof(FiltroNome)); AplicarFiltro(); }
        }

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

        #region Listagem e Seleção
        private CentrosCusto? _selecionado;
        public CentrosCusto? Selecionado
        {
            get => _selecionado;
            set { _selecionado = value; OnPropertyChanged(nameof(Selecionado)); }
        }

        public ObservableCollection<CentrosCusto> CentrosCusto { get; } = new();
        private List<CentrosCusto> _todosCentrosCusto = new();
        #endregion

        #region Service
        private readonly IServiceCentrosCusto _service;
        #endregion

        public CentrosCustoViewModel(IServiceCentrosCusto service) : base()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

            SalvarCommand = new SimpleRelayCommand(Salvar);
            EditarCommand = new SimpleRelayCommand(Editar);
            LimparCommand = new SimpleRelayCommand(Limpar);
            DeletarCommand = new SimpleRelayCommand(Deletar);

            CarregarLista();
        }

        #region Transações DB
        private void Salvar()
        {
            try
            {
                LimparMensagens();

                var entity = Selecionado ?? new CentrosCusto();
                entity.Nome = Nome;
                entity.Observacoes = Observacoes;
                entity.Ativo = Ativo;

                _service.SalvarCentroCusto(entity);
                CarregarLista();
                Limpar();
                MensagemSucesso = "Centro de custo salvo com sucesso!";
            }
            catch (GerenciamentoDeFrotaExceptions ex)
            {
                MensagemErro = ex.Message;
            }
            catch (Exception)
            {
                MensagemErro = "Erro inesperado ao salvar. Contate o suporte.";
            }
        }

        private void Editar()
        {
            if (Selecionado == null)
            {
                MensagemErro = "Selecione um registro para editar.";
                return;
            }
            CarregarFormulario(Selecionado);
        }

        private void Limpar()
        {
            Selecionado = null;
            Nome = string.Empty;
            Observacoes = string.Empty;
            Ativo = true;
            LimparMensagens();
        }

        private void Deletar()
        {
            try
            {
                LimparMensagens();

                if (Selecionado == null)
                {
                    MensagemErro = "Selecione um registro para deletar.";
                    return;
                }

                _service.DeletarCentroCusto(Selecionado.Id);
                CarregarLista();
                Limpar();
                MensagemSucesso = "Centro de custo removido com sucesso!";
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
        private void CarregarLista()
        {
            _todosCentrosCusto = _service.ListarCentrosCustos();
            AplicarFiltro();
        }

        private void CarregarFormulario(CentrosCusto item)
        {
            Nome = item.Nome ?? string.Empty;
            Observacoes = item.Observacoes ?? string.Empty;
            Ativo = item.Ativo ?? true;
        }

        private void AplicarFiltro()
        {
            CentrosCusto.Clear();

            var lista = string.IsNullOrWhiteSpace(FiltroNome)
                ? _todosCentrosCusto
                : _todosCentrosCusto
                    .Where(c => c.Nome.Contains(FiltroNome, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            foreach (var item in lista)
                CentrosCusto.Add(item);
        }

        private void LimparMensagens()
        {
            MensagemErro = string.Empty;
            MensagemSucesso = string.Empty;
        }
        #endregion
    }
}