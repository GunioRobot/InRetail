using InRetail.UiCore.Menus;

namespace InRetail.Shell.Menus
{
    public class SgMenuAction : IMenuItem
    {
        private string _title;

        public string Name
        {
            get { return _title; }
            set { _title = value; }
        }
    }
}