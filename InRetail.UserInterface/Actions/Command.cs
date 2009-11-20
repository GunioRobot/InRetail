using System;
using System.Windows.Input;
using StructureMap;

namespace InRetail.UserInterface.Actions
{
    public class Command<T> : ICommand
    {
        private readonly Action<T> _action;
        private readonly IContainer _container;

        public Command(IContainer container, Action<T> action)
        {
            _container = container;
            _action = action;
        }

        public void Execute(object parameter)
        {
            var target = _container.GetInstance<T>();
            _action(target);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}