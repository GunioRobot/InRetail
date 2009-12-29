using System.Collections.ObjectModel;

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