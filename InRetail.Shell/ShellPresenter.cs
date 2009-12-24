using System;
using InRetail.Shell.Menus;
using InRetail.UiCore;
using InRetail.UiCore.Actions;

namespace InRetail.Shell
{
    public class ShellPresenter : IShellService
    {
        public ShellPresenter(IShellView view, MenuViewModel menuViewModel)
        {
            View = view;
            View.ShowMenu(menuViewModel);
        }

        public IShellView View { get; private set; }
        
        public void AddModuleAction(IScreenAction action)
        {
            
        }
    }


}