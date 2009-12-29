namespace InRetail.UiCore.Menus.Internal
{
    internal class MenuExpression : IMenuExpression
    {
        private readonly IMenuContainer _container;
        private readonly string _name;

        public MenuExpression(IMenuContainer container, string name)
        {
            _container = container;
            _name = name;
        }

        public IMenuContainer ToContainer()
        {
            var container = new MenuContainer(){Name = _name};
            _container.Add(container);
            return container;
        }

        public void ToScreen<T>()
        {
            var item = new MenuItem() { Name = _name };
            _container.Add(item);
        }

        public void ToWizard<T>()
        {
            var item = new MenuItem() { Name = _name };
            _container.Add(item);
        }
    }
}