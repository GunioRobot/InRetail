namespace InRetail.UiCore.Screens
{
    public interface IScreenSubject
    {
        bool Matches(IScreen screen);
        IScreen CreateScreen(IScreenFactory factory);
    }

    public interface IScreenSubject<T> : IScreenSubject
    {
    }
}