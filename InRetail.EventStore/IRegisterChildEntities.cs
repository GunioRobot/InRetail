namespace InRetail.EventStore
{
    public interface IRegisterChildEntities
    {
        void RegisterChildEventProvider(IEntityEventProvider entityEventProvider);
    }
}