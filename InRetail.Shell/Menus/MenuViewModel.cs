using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using InRetail.Shell.Actions;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using StructureMap;

namespace InRetail.Shell.Menus
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Menus = new ObservableCollection<MenuItemViewModel>();

            var subject = new SingletonScreenSubject<TestScreen>();
            var prodCatalog = new MenuItemViewModel() { Name = "Product Catalog" };
            prodCatalog.Menus.Add(new MenuItemViewModel() { Name = "Products", 
                ActionCommand = new Command<IScreenConductor>(ObjectFactory.Container, x => x.OpenScreen(subject)) });
            prodCatalog.Menus.Add(new MenuItemViewModel() { Name = "Create new product" });
            
            
            var inventory = new MenuItemViewModel() { Name = "Inventory" };
            var purchaseManagment = new MenuItemViewModel() { Name = "Purchase Management" };

            
            
            Menus.Add(prodCatalog);
            Menus.Add(inventory);
            Menus.Add(purchaseManagment);
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
    }

    public class TestScreen:IScreen

    {
        private Button _button;

        public TestScreen()
        {
            _button = new Button() { Content = "Ok" };
        }
        public void Dispose()
        {
            
        }

        public object View
        {
            get
            {
                
                return _button;
            }
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