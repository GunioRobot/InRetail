namespace InRetail.EventStore
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}