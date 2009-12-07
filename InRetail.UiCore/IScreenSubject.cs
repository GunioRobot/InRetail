namespace InRetail.UiCore
{
    public interface IScreenSubject
    {
        bool Matches(IScreen screen);
        IScreen CreateScreen(IScreenFactory factory);
    }
    // Marker interface
    public interface IScreenSubject<T> : IScreenSubject
    {
    }
}