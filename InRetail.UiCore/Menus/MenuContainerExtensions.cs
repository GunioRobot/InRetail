using InRetail.UiCore.Menus.Internal;

namespace InRetail.UiCore.Menus
{
    public static class MenuContainerExtensions
    {
        public static IMenuExpression Register(this IMenuContainer menuContainer, string name)
        {
            return new MenuExpression(menuContainer, name);
        }
    }
}