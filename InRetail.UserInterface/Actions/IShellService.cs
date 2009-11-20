using InRetail.UserInterface.Screens;

namespace InRetail.UserInterface.Actions
{
    public interface IShellService : IStartable
    {
        void ActivateScreen(IScreen screen);
        void ClearTransient();
    }
}