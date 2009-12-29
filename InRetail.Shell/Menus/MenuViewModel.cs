using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Helpers;
using InRetail.UiCore.Menus;

namespace InRetail.Shell.Menus
{
    public class MenuViewModel
    {

        public MenuViewModel()
        {
            var registry = new MenuRegistry();

            IMenuContainer container = registry.Register("Product Catalog").ToContainer();
            IMenuContainer menuContainer = container.Register("Menu Item").ToContainer();
            menuContainer.Register("MenuItem2").ToScreen<TestScreen>();

            Menus = new ObservableCollection<MenuItemViewModel>();

            new ObservableCollectionSynchronizer<IMenuItem, MenuItemViewModel>(registry, Menus,
                                                                               (x) => new MenuItemViewModel(x));
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; private set; }
    }

    public class TestScreen : IScreen

    {
        private readonly Button _button;

        public TestScreen()
        {
            _button = new Button() {Content = "Ok"};
        }

        public void Dispose()
        {
        }

        public object View
        {
            get { return _button; }
        }

        public string Title
        {
            get { return "Test"; }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
        }

        public bool CanClose()
        {
            return true;
        }
    }
}