using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Models
{
    [Serializable]
    public class CentrosCusto : BaseModel
    {
        private long _id;

        public long Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }


        private string? _nome;
        public string Nome
        {
            get => _nome ?? string.Empty;
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }


        private string? _observacoes;

        public string Observacoes
        {
            get => _observacoes ?? string.Empty;

            set { _observacoes = value; OnPropertyChanged(nameof(Observacoes)); }
        }


        private bool? _ativo;

        public bool Ativo
        {
            get => _ativo ?? true;
            set { _ativo = value; OnPropertyChanged(nameof(_ativo)); }
        }

    }
}
