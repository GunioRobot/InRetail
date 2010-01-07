using System;
using System.Windows.Input;

namespace InRetail.UiCore.Menus.Internal
{
    internal class MenuAction:IMenuAction
    {
        public string Name
        {
            get; set;
        }

        public ICommand Command
        {
            get; set;
        }
    }
}