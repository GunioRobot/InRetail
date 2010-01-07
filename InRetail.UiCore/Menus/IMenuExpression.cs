using InRetail.UiCore.Screens;

namespace InRetail.UiCore.Menus
{
    public interface IMenuExpression
    {
        IMenuContainer ToContainer();
        void ToScreen<T>() where T : IScreen;
        void ToWizard<T>();
    }
}