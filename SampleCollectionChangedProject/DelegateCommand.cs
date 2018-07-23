using System;
using System.Windows.Input;

namespace SampleCollectionChangedProject
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action action) { _action = action; }

        Action _action;

        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter) { _action?.Invoke(); }

        public event EventHandler CanExecuteChanged;
    }
    public class DelegateCommand<T> : ICommand
    {
        public DelegateCommand(Action<T> action) { _action = action; }

        Action<T> _action;

        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter) { _action?.Invoke((T)parameter); }

        public event EventHandler CanExecuteChanged;
    }
}
