using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.Packpub.Northwind.Application
{
    public class Command : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public Command(Action<object> execute): this(execute, null)
        { }
        public Command(Action<object> execute,Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return (_canExecute == null) || _canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged = delegate { };
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, new EventArgs());
        }

    }
}
