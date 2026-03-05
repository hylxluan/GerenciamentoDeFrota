using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Interfaces.Gerenciadores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        #endregion

        #region Listagem e Seleção
        private CentrosCusto? _selecionado;
        public CentrosCusto? Selecionado
        {
            get => _selecionado;
            set { _selecionado = value; OnPropertyChanged(nameof(Selecionado)); }
        }
        public ObservableCollection<CentrosCusto> CentrosCusto { get; } = new();
        #endregion


        #region Gerenciador
        private readonly IServiceCentrosCusto _gerenciadorCentrosCusto;
        #endregion



        public CentrosCustoViewModel() : base()
        {

        }

        #region Transações DB
        private void Salvar()
        {
        }

        private void Editar()
        {
        }

        private void Limpar()
        {
        }

        private void Deletar()
        {
        }
        #endregion

        #region Validações
        #endregion

        #region Métodos auxiliares
        private void AplicarFiltro()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
