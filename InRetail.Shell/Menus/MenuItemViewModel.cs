using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using InRetail.UiCore.Extensions;
using InRetail.UiCore.Helpers;
using InRetail.UiCore.Menus;

namespace InRetail.Shell.Menus
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel(IMenuItem item)
        {
            

            Menus = new ObservableCollection<MenuItemViewModel>();
            Name = item.Name;
            if(item is IMenuContainer)
                new ObservableCollectionSynchronizer<IMenuItem, MenuItemViewModel>(item.As<IMenuContainer>(), Menus,
                                                                              (x) => new MenuItemViewModel(x));

            if (item is IMenuAction)
                ActionCommand = item.As<IMenuAction>().Command;
            
        }
        public string Name { get; private set; }
        public ICommand ActionCommand { get; private set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; private set; }
    }
}