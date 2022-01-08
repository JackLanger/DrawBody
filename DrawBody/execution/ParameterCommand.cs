using System;
using System.Windows.Input;

namespace DrawBody.execution
{
    public class ParameterCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<T>? _Action;

        public ParameterCommand(Action<T> action)
        {
            _Action = action;
        }
        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is not null)
                _Action?.Invoke((T)parameter);
        }
    }
}
