using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Interfaces.Gerenciadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CentrosCustoViewModel : BaseViewModel
    {


        public CentrosCustoCommand CentrosCustoCommand { get; set; }
        private readonly IGerenciadorCentrosCusto _gerenciadorCentrosCusto;

        #region Fields
        private long _id;

        public long Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }


        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }


        private string _observacoes;

        public string Observacoes
        {
            get { return _observacoes; }
            set { _observacoes = value; OnPropertyChanged(nameof(Observacoes)); }
        }


        private bool _ativo;

        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; OnPropertyChanged(nameof(_ativo)); }
        }
        #endregion



        public CentrosCustoViewModel() : base()
        {
            CentrosCustoCommand = new CentrosCustoCommand(this);
        }

    }
}
