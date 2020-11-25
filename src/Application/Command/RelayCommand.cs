using System;
using System.Windows.Input;

namespace Application.Command
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> action;
        readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter) => action(parameter);
    }
}