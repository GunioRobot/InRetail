using System.Collections.ObjectModel;
using InRetail.UiCore.Screens;

namespace InRetail.UiCore.Menus
{
    public abstract class MenuContainerBase : ObservableCollection<IMenuItem>, IMenuContainer
    {
        public string Name
        {
            get; set;
        }
    }
}