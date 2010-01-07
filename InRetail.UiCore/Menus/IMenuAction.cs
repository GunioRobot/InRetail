using System.Windows.Input;

namespace InRetail.UiCore.Menus
{
    public interface IMenuAction : IMenuItem
    {
        ICommand Command { get; }
    }
}