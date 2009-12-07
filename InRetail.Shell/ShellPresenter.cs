using System;
using InRetail.UiCore;
using InRetail.UiCore.Actions;

namespace InRetail.Shell
{
    public class ShellPresenter : IShellService
    {
        public ShellPresenter(IShellView view)
        {
            View = view;
        }

        public IShellView View { get; private set; }
        
        public void AddModuleAction(IScreenAction action)
        {
            
        }
    }


}