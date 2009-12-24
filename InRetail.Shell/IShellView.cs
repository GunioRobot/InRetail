using InRetail.Shell.Menus;

namespace InRetail.Shell
{
    public interface IShellView
    {
        void ShowView();
        void ShowMenu(MenuViewModel model);
    }
}