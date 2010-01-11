using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace Tests.InRetail.UserInterface.ScreenManagement
{
    public abstract class TestScreen3 : IScreen
    {
        public abstract void Dispose();
        public abstract object View { get; }
        public abstract string Title { get; }
        public abstract void Activate(IScreenObjectRegistry screenObjects);
        public abstract bool CanClose();
    }
}