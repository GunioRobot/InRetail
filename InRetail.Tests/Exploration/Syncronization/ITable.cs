namespace Tests.InRetail.Exploration
{
    public interface ITable
    {
        Revision GetRevision();
        ChangeSet GetChanges(Revision revision);
        void Update(ChangeSet set);
    }
}