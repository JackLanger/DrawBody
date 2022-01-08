using System;
using System.Windows.Input;

namespace DrawBody.execution;

public class RelayCommand : ICommand
{

    private Action _Action;

    public RelayCommand(Action action)
    {
        _Action = action;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _Action();
}
