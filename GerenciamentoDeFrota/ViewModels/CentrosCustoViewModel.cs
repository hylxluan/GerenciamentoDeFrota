using GerenciamentoDeFrota.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.ViewModels
{
    public class CentrosCustoViewModel : BaseViewModel
    {
        public CentrosCustoCommand CentrosCustoCommand { get; set; }


        private long _id;

        public long Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }


        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
            }
        }


        private string _observacoes;

        public string Observacoes
        {
            get { return _observacoes; }
            set { _observacoes = value; OnPropertyChanged("Observacoes"); }
        }


        private bool _ativo;

        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; OnPropertyChanged("Ativo"); }
        }




        public CentrosCustoViewModel() : base()
        {
            CentrosCustoCommand = new CentrosCustoCommand(this);
        }

    }
}
