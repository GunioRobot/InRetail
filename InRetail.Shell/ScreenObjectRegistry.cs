using System;
using System.Collections.Generic;
using InRetail.UiCore.Actions;

namespace InRetail.Shell
{
    public class ScreenObjectRegistry : IScreenObjectRegistry
    {
        public IEnumerable<ScreenAction> Actions
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearTransient()
        {
            throw new NotImplementedException();
        }

        public IActionExpression Action(string name)
        {
            throw new NotImplementedException();
        }

        public IActionExpression PermanentAction(string name)
        {
            throw new NotImplementedException();
        }

        public void PlaceInExplorerPane(object view, string header)
        {
            throw new NotImplementedException();
        }
    }
}