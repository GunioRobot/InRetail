namespace InRetail.UiCore.Menus
{
    public interface IMenuRegistry
    {
        IMenuExpression Register(string s);
        void AddMenu(IMenuItem item);
    }
}