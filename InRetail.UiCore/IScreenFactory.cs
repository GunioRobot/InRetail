namespace InRetail.UiCore
{
    public interface IScreenFactory
    {
        T Build<T>() where T : IScreen;
        IScreen<T> Build<T>(T subject);
    }
}