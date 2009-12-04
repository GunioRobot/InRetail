using System;
using System.Collections.Generic;
using System.Windows.Input;
using InRetail.UiCore.Actions;

namespace InRetail.UiCore.Controls
{
    public interface ICommandBar
    {
        void AddCommand(string text, Action action);
        void AddCommand(string text, ICommand command);
        void Refill(IEnumerable<Actions.ScreenAction> actions);
    }
}