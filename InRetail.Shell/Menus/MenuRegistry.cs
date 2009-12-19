using System;
using InRetail.UiCore.Menus;

namespace InRetail.Shell.Menus
{
    public class MenuRegistry : MenuRegistryBase
    {
        public MenuRegistry()
            : base()
        {
        }

        public override IMenuExpression Register(string menuTitle)
        {
            return new MenuExpression(this, menuTitle);
        }
    }
}