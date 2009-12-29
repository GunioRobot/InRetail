using System.Collections.Generic;
using System.Collections.Specialized;

namespace InRetail.UiCore.Menus
{
    public interface IMenuContainer : IMenuItem, IList<IMenuItem>,INotifyCollectionChanged
    {
        
    }
}