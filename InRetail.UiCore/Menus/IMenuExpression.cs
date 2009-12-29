namespace InRetail.UiCore.Menus
{
    public interface IMenuExpression
    {
        IMenuContainer ToContainer();
        void ToScreen<T>();
        void ToWizard<T>();
    }
}