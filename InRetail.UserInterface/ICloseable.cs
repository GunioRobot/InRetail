namespace InRetail.UserInterface
{
    public interface ICloseable
    {
        void AddCanCloseMessages(CloseToken token);
        void PerformShutdown();
    }
}