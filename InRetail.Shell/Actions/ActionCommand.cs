using System;
using System.Windows.Input;

namespace InRetail.Shell.Actions
{
    public class ActionCommand : ICommand
    {
        private readonly Action _action;
        private bool _canExecute = true;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public bool Enabled
        {
            set
            {
                _canExecute = value;
                if (CanExecuteChanged != null) CanExecuteChanged(this, new EventArgs());
            }
        }

        #region ICommand Members

        public void Execute(object parameter)
        {
            _action();
        }

        public virtual bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}