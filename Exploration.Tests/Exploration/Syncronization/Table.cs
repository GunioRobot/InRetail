using System;
using System.Collections.Generic;

namespace Tests.InRetail.Exploration
{
    public abstract class Table : ITable
    {
        public Revision GetRevision()
        {
            return OnGetRevision();
        }


        public ChangeSet GetChanges(Revision revision)
        {
            var changes = new ChangeSet();
            changes.Inserts = RetriveInserts(revision);
            changes.Updates = RetriveUpdates(revision);
            changes.Deletes = RetriveDeletes(revision);
            return changes;
        }


        public void Update(ChangeSet set)
        {
            InsertRows(set.Inserts);
            UpdateRows(set.Updates);
            DeleteRows(set.Deletes);
        }

        protected abstract Revision OnGetRevision();

        protected abstract IEnumerable<Row> RetriveDeletes(Revision revision);

        protected abstract IEnumerable<Row> RetriveUpdates(Revision revision);

        protected abstract IEnumerable<Row> RetriveInserts(Revision revision);
        protected abstract void DeleteRows(IEnumerable<Row> enumerable);

        protected abstract void UpdateRows(IEnumerable<Row> enumerable);

        protected abstract void InsertRows(IEnumerable<Row> rows);
    }
}