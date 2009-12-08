using System;
using System.ComponentModel;
using Microsoft.Practices.Composite.Regions;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using ScreenAction=InRetail.UiCore.Actions.ScreenAction;

namespace Tests.InRetail.UserInterface.ApplicationShell
{
    public class StubRegion : IRegion
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IRegionManager Add(object view)
        {
            throw new NotImplementedException();
        }

        public IRegionManager Add(object view, string viewName)
        {
            throw new NotImplementedException();
        }

        public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            throw new NotImplementedException();
        }

        public void Remove(object view)
        {
            throw new NotImplementedException();
        }

        public void Activate(object view)
        {
            throw new NotImplementedException();
        }

        public void Deactivate(object view)
        {
            throw new NotImplementedException();
        }

        public object GetView(string viewName)
        {
            throw new NotImplementedException();
        }

        public IViewsCollection Views
        {
            get { throw new NotImplementedException(); }
        }

        public IViewsCollection ActiveViews
        {
            get { throw new NotImplementedException(); }
        }

        public object Context
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRegionManager RegionManager
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRegionBehaviorCollection Behaviors
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class SomeOtherScreen : IScreen
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

    public class TestScreenSubject<TScreen> : IScreenSubject where TScreen : IScreen
    {
        public bool Matches(IScreen screen)
        {
            return (screen is TScreen);
        }

        public IScreen CreateScreen(IScreenFactory factory)
        {
            var screen = factory.Build<TestScreen>();
            return screen;
        }
    }

    public class TestScreen : IScreen
    {
        public int CountOfActivateCalled = 0;
        public object ObjectReturnedByView;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object View
        {
            get { return ObjectReturnedByView; }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
            CountOfActivateCalled++;
        }

        public bool CanClose()
        {
            throw new NotImplementedException();
        }
    }
}