using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.UiCore;
using InRetail.UiCore.Actions;

namespace Tests.InRetail.UserInterface.Menus
{
    public class TestWizard
    {
    }

    public class TestScreen : IScreen
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object View
        {
            get { throw new NotImplementedException(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
            throw new NotImplementedException();
        }

        public bool CanClose()
        {
            throw new NotImplementedException();
        }
    }
}