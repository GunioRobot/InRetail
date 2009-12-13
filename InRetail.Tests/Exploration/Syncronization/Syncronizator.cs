namespace Tests.InRetail.Exploration
{
    public class Syncronizator
    {
        public void Syncronize()
        {
            Revision revision = Slave.GetRevision();
            ChangeSet changes = Master.GetChanges(revision);
            Slave.Update(changes);
        }

        public ITable Master { get; set; }

        public ITable Slave { get; set; }
    }
}