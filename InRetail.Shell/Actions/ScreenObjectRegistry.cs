using System;
using System.Collections.Generic;
using System.Windows.Input;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Dialogs;
using StructureMap;

namespace InRetail.Shell.Actions
{
    public class ScreenObjectRegistry : IScreenObjectRegistry
    {
        private readonly IContainer _container;
        private IList<IScreenAction> _actions;

        public ScreenObjectRegistry(IContainer container)
        {
            _actions=new List<IScreenAction>();
            _container = container;
        }

        public IEnumerable<IScreenAction> Actions
        {
            get { return _actions; }
        }

        public void ClearTransient()
        {
            throw new NotImplementedException();
        }

        public IActionExpression Action(string name)
        {
            throw new NotImplementedException();
        }

        public IActionExpression PermanentAction(string name)
        {
            return new BindingExpression(name, this, x => x.IsPermanent = true)
            {
                ShortcutOnly = true
            };
        }

        public void PlaceInExplorerPane(object view, string header)
        {
            throw new NotImplementedException();
        }

        private ICommand command<T>(Action<T> action)
        {
            return new Command<T>(_container, action);
        }

        public ICommand CommandForDialog<T>()
        {
            return command<IDialogLauncher>(x => x.Launch<T>());
        }

        private ICommand CommandForEvent<T>()
        {
            throw new NotImplementedException();
        }

        private ICommand CommandForScreen<T>()
        {
            throw new NotImplementedException();
        }

        private void register(ScreenAction screenAction)
        {
            _actions.Add(screenAction);
        }

        public class BindingExpression : IBindingExpression, IActionExpression
        {
            private readonly ScreenObjectRegistry _registry;
            private readonly ScreenAction _screenAction;
            private KeyGesture _gesture;

            public BindingExpression(string name, ScreenObjectRegistry registry)
            {
                _screenAction = new ScreenAction();
                _screenAction.Name = name;
                _registry = registry;
            }

            public BindingExpression(string name, ScreenObjectRegistry registry, Action<ScreenAction> configure)
                : this(name, registry)
            {
                configure(_screenAction);
            }

            public bool ShortcutOnly { set { _screenAction.ShortcutOnly = value; } }

            #region IActionExpression Members

            public IBindingExpression Bind(Key key)
            {
                _gesture = new KeyGesture(key);
                return this;
            }

            public IBindingExpression Bind(ModifierKeys modifiers, Key key)
            {
                _gesture = new KeyGesture(key, modifiers);
                return this;
            }

            #endregion

            #region IBindingExpression Members

            public ScreenAction ToDialog<T>()
            {
                return buildAction(() => _registry.CommandForDialog<T>());
            }

            public ScreenAction ToScreen<T>() where T : IScreen
            {
                return buildAction(() => _registry.CommandForScreen<T>());
            }

            public ScreenAction PublishEvent<T>() where T : new()
            {
                return buildAction(() => _registry.CommandForEvent<T>());
            }

            public ScreenAction To(Action action)
            {
                return buildAction(() => new ActionCommand(action));
            }

            public ScreenAction To(ICommand command)
            {
                return buildAction(() => command);
            }

            #endregion

            private ScreenAction buildAction(Func<ICommand> value)
            {
                ICommand command = value();
                _screenAction.Binding = new KeyBinding(command, _gesture);
                _registry.register(_screenAction);
                return _screenAction;
            }

            public BindingExpression Icon(string icon)
            {
                _screenAction.Icon = icon;
                return this;
            }
        }
    }
}