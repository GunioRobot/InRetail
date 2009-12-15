using System.Collections.Generic;

namespace InRetail.UiCore.Actions
{
    public interface IScreenObjectRegistry
    {
        IEnumerable<IScreenAction> Actions { get; }
        void ClearTransient();
        IActionExpression Action(string name);
        IActionExpression PermanentAction(string name);
        void PlaceInExplorerPane(object view, string header);
    }
}