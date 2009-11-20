using System.Windows.Input;
using InRetail.UserInterface.Controls;

namespace InRetail.UserInterface.Actions
{
    public interface IScreenAction
    {
        bool IsPermanent { get; set; }
        InputBinding Binding { get; set; }
        string Name { get; set; }
        Icon Icon { get; set; }
        ICommand Command { get; }
        bool ShortcutOnly { get; set; }
        void BuildButton(ICommandBar bar);
    }
}