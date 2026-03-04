using GerenciamentoDeFrota.Commands;
using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Interfaces.Gerenciadores;
using System;
using System.Collections.Generic;
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
        public CentrosCusto? CentrosCusto { get; set; }
        #endregion

        #region Gerenciador
        private readonly IGerenciadorCentrosCusto _gerenciadorCentrosCusto;
        #endregion



        public CentrosCustoViewModel() : base()
        {

        }

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
    }
}
