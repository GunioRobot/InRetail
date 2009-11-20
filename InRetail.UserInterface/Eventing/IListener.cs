namespace InRetail.UserInterface.Eventing
{
    public interface IListener
    {
    }
    public interface IListener<T>
    {
        void Handle(T message);
    }
}