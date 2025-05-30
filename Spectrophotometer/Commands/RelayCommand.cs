using System;
using System.Windows.Input;

namespace Spectrophotometer.Commands;

public class RelayCommand : ICommand
{
    private readonly Action<object?>? _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        if (execute is null)
            throw new ArgumentNullException(nameof(execute));

        _execute = execute;
        _canExecute = canExecute;
    }

    public void Execute(object? parameter) => _execute?.Invoke(parameter);
    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
