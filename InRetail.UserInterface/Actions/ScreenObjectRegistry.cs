using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using InRetail.UserInterface.Controls;
using InRetail.UserInterface.Dialogs;
using InRetail.UserInterface.Eventing;
using InRetail.UserInterface.Extensions;
using InRetail.UserInterface.Screens;
using StructureMap;

namespace InRetail.UserInterface.Actions
{
    public class ScreenObjectRegistry : IScreenObjectRegistry
    {
        private readonly List<ScreenAction> _actions = new List<ScreenAction>();
        private readonly ArrayList _explorerObjects = new ArrayList();
        private readonly Window _window;
        private readonly IContainer _container;
        private readonly IApplicationShell _shell;

        public ScreenObjectRegistry(Window window, IContainer container, IApplicationShell shell)
        {
            _window = window;
            _container = container;
            _shell = shell;
        }

        public IEnumerable<ScreenAction> Actions
        {
            get { return _actions; }
        }

        public void ClearTransient()
        {
            _actions.Where(x => !x.IsPermanent).Each(x => _window.InputBindings.Remove(x.Binding));
            _actions.RemoveAll(x => !x.IsPermanent);
            foreach (object view in _explorerObjects)
            {
                _shell.RemoveFromExplorerPane(view);
            }
            _explorerObjects.Clear();
        }

        public IActionExpression Action(string name)
        {
            return new BindingExpression(name, this);
        }

        public IActionExpression PermanentAction(string name)
        {
            return new BindingExpression(name, this, x => x.IsPermanent = true)
                       {
                           ShortcutOnly = false
                       };
        }

        public void PlaceInExplorerPane(object view, string header)
        {
            _explorerObjects.Add(view);
            _shell.PlaceInExplorerPane(view, header);
        }

        private ICommand command<T>(Action<T> action)
        {
            return new Command<T>(_container, action);
        }

        public ICommand CommandForDialog<T>()
        {
            return command<IDialogLauncher>(x => x.Launch<T>());
        }

        public ICommand CommandForEvent<T>() where T : new()
        {
            return command<IEventAggregator>(x => x.SendMessage(new T()));
        }

        public ICommand CommandForScreen<T>() where T : IScreen
        {
            Func<IScreenSubject> subject = () => _container.GetInstance<SingletonScreenSubject<T>>();
            return command<IScreenConductor>(x => x.OpenScreen(subject()));
        }

        protected void register(ScreenAction screenAction)
        {
            _actions.Add(screenAction);
            _window.InputBindings.Add(screenAction.Binding);
        }

        #region Nested type: BindingExpression

        public class BindingExpression : IBindingExpression, IActionExpression
        {
            private readonly ScreenObjectRegistry _registry;
            private readonly ScreenAction _screenAction = new ScreenAction();
            private KeyGesture _gesture;

            public BindingExpression(string name, ScreenObjectRegistry registry)
            {
                _screenAction.Name = name;
                _registry = registry;
            }

            public BindingExpression(string name, ScreenObjectRegistry registry, Action<ScreenAction> configure)
                : this(name, registry)
            {
                configure(_screenAction);
            }

            public bool ShortcutOnly
            {
                set { _screenAction.ShortcutOnly = value; }
            }

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

            public BindingExpression Icon(Icon icon)
            {
                _screenAction.Icon = icon;
                return this;
            }
        }

        #endregion
    }
}