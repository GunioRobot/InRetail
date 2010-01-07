using System;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;
using StructureMap;

namespace InRetail.UiCore.Menus.Internal
{
    internal class MenuExpression : IMenuExpression
    {
        private readonly IMenuContainer _menuContainer;
        private readonly string _name;
        private IContainer _container;

        public MenuExpression(IMenuContainer menuContainer, string name)
        {
            _menuContainer = menuContainer;
            _container = ObjectFactory.GetInstance<IContainer>();
            _name = name;
        }

        public IMenuContainer ToContainer()
        {
            var container = new MenuContainer() { Name = _name };
            _menuContainer.Add(container);
            return container;
        }

        public void ToScreen<T>() where T : IScreen
        {
            Func<IScreenSubject> subject = () => _container.GetInstance<SingletonScreenSubject<T>>();
            
            var item = new MenuAction
                           {
                               Name = _name,
                               Command = new Command<IScreenConductor>(_container, x => x.OpenScreen(subject()))
                           };

            _menuContainer.Add(item);
        }

        public void ToWizard<T>()
        {
            var item = new MenuAction() { Name = _name };
            _menuContainer.Add(item);
        }
    }
}