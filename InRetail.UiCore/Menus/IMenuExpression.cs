namespace InRetail.UiCore.Menus
{
    public interface IMenuExpression
    {
        IMenuRegistry ToMenu();
        void ToScreen<T>();
        void ToWizard<T>();
    }
}