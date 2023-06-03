using System;
using System.Windows.Input;

namespace ILSport.Framework;

public class DelegateCommand : ICommand
{
    private readonly Action<object?> _open;

    public DelegateCommand(Action<object?> open)
    {
        _open = open;
    }
        
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        //if (parameter == null) throw new ArgumentNullException(nameof(parameter));
        _open.Invoke(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}