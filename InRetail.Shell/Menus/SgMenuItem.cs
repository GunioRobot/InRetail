using System;
using InRetail.UiCore.Menus;

namespace InRetail.Shell.Menus
{
    public class SgMenuItem : MenuRegistryBase, IMenuItem
    {
        private string _title;

        public string Name
        {
            get { return _title; }
            set { _title = value; }
        }

        public override IMenuExpression Register(string menuTitle)
        {
            return new MenuExpression(this, menuTitle);
        }
    }

}