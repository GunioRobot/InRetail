using System;

namespace InRetail.UserInterface.Actions
{
    public class LatchedActionCommand : ActionCommand
    {
        private readonly Func<bool> _canExecute;

        public LatchedActionCommand(Action action, Func<bool> canExecute)
            : base(action)
        {
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute() && base.CanExecute(parameter);
        }
    }
}