using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace Tests.InRetail.UserInterface.ApplicationShell
{
    public abstract class TestScreen2 : IScreen
    {
        public abstract void Dispose();
        public abstract object View { get; }
        public abstract string Title { get; }
        public abstract void Activate(IScreenObjectRegistry screenObjects);
        public abstract bool CanClose();
    }
}