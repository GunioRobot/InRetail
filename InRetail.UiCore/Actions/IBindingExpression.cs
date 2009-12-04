using System;
using System.Windows.Input;

namespace InRetail.UiCore.Actions
{
    public interface IBindingExpression
    {
        ScreenAction ToDialog<T>();
        ScreenAction ToScreen<T>() where T : IScreen;
        ScreenAction PublishEvent<T>() where T : new();
        ScreenAction To(Action action);
        ScreenAction To(ICommand command);
    }
}