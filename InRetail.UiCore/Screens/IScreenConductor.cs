namespace InRetail.UiCore.Screens
{
    public interface IScreenConductor : IStartable
    {
        void OpenScreen(IScreenSubject subject);
        bool CanClose();
        void Close(IScreen screen);
        void CloseAllBut(IScreen screen);
        void CloseAll();
    }
}