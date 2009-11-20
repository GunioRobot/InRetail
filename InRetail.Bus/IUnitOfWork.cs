namespace InRetail.Bus
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}