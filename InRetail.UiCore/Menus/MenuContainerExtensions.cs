using InRetail.UiCore.Menus.Internal;

namespace InRetail.UiCore.Menus
{
    public static class MenuContainerExtensions
    {
        public static IMenuExpression Register(this IMenuContainer container, string name)
        {
            return new MenuExpression(container,name);
        }
    }
}