using GerenciamentoDeFrota.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GerenciamentoDeFrota.Commands
{
    public class CentrosCustoCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private CentrosCustoViewModel _centrosCustoViewModel;



        public CentrosCustoCommand(CentrosCustoViewModel CentrosCustoVM)
        {
            _centrosCustoViewModel = CentrosCustoVM;
        }

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
