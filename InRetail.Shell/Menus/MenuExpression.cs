using InRetail.UiCore.Menus;

namespace InRetail.Shell.Menus
{
    class MenuExpression : IMenuExpression
    {
        private readonly IMenuRegistry _registry;
        private readonly string _title;

        public MenuExpression(IMenuRegistry registry, string title)
        {
            _registry = registry;
            _title = title;
        }

        public IMenuRegistry ToMenu()
        {
            var item = new SgMenuItem() { Name = _title };
            _registry.AddMenu(item);
            return item;
        }

        public void ToScreen<T>()
        {
            var item = new SgMenuAction() { Name = _title };
            _registry.AddMenu(item);
        }

        public void ToWizard<T>()
        {
            var item = new SgMenuAction() { Name = _title };
            _registry.AddMenu(item);
        }
    }
}