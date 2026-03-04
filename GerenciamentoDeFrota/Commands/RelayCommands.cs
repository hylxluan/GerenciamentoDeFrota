using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GerenciamentoDeFrota.Commands
{
    public class RelayCommands<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        private readonly Action<T> _execute;
        private readonly Func<Object, bool> _canExecute;

        public RelayCommands(Action<T> execute, Func<Object, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        public bool CanExecute(object? parameter) => this._canExecute?.Invoke((T?)parameter) ?? true;

        public void Execute(object? parameter) => this._execute((T?)parameter);
    }
}
