using System.Windows.Input;
using InRetail.UiCore.Controls;


namespace InRetail.UiCore.Actions
{
    public interface IScreenAction
    {
        //bool IsPermanent { get; set; }
        //InputBinding Binding { get; set; }
        string Name { get; set; }
        ICommand Command { get; }
        //bool ShortcutOnly { get; set; }
        //void BuildButton(ICommandBar bar);
    }
}