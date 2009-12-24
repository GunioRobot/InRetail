using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InRetail.Shell.Menus
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel()
        {
            Menus = new ObservableCollection<MenuItemViewModel>();
        }
        public string Name { get; set; }
        public ICommand ActionCommand { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; private set; }
    }
}