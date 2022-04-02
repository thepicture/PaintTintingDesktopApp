using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


public class Command : ICommand
{
    public Action<object> RunMethod { get; set; }
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        if (RunMethod != null) return true;
        else return false;
    }

    public void Execute(object parameter)
    {
        RunMethod(parameter);
    }

    public Command Register(Action<object> runMethod)
    {
        this.RunMethod = runMethod;
        CanExecuteChanged?.Invoke(this, new EventArgs());
        return this;
    }
    public static Command RegisterCommand(Action<object> runMethod)
    {
        var command = new Command();
        command.RunMethod = runMethod;
        command.CanExecuteChanged?.Invoke(command, new EventArgs());
        return command;
    }
}