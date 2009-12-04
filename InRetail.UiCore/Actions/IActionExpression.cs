using System.Windows.Input;

namespace InRetail.UiCore.Actions
{
    public interface IActionExpression
    {
        IBindingExpression Bind(Key key);
        IBindingExpression Bind(ModifierKeys modifiers, Key key);
    }
}