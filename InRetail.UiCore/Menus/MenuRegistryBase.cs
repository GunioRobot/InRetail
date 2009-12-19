using System.Collections.Generic;

namespace InRetail.UiCore.Menus
{
    public abstract class MenuRegistryBase : IMenuRegistry
    {
        private IList<IMenuItem> _menus;

        protected MenuRegistryBase()
        {
            _menus = new List<IMenuItem>();
        }

        public IEnumerable<IMenuItem> Menus
        {
            get { return _menus; }

        }

        public abstract IMenuExpression Register(string menuTitle);

        public void AddMenu(IMenuItem item)
        {
            _menus.Add(item);
        }
    }
}