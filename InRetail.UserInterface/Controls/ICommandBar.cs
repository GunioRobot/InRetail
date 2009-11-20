using System;
using System.Collections.Generic;
using System.Windows.Input;
using InRetail.UserInterface.Actions;

namespace InRetail.UserInterface.Controls
{
    // The last step that captures the actual
    // "action" of the ScreenAction

    public interface ICommandBar
    {
        void AddCommand(string text, Action action);
        void AddCommand(string text, ICommand command, Icon icon);
        void Refill(IEnumerable<ScreenAction> actions);
    }
}