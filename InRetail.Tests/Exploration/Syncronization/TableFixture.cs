using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class TableFixture

    {
        
        [Test]
        public void GetRevisionReturnsRevision()
        {
            var revision = new Revision();
            var table = new TestTable();
            table.Revision = revision;

            Revision revision1 = table.GetRevision();

            Assert.AreEqual(revision, revision1);
        }

        [Test]
        public void CanGetChangeSet()
        {
            var updates = new []{new Row(),new Row()};
            var inserts = new []{new Row(),new Row()};
            var deletes = new []{new Row(),new Row()};
            var revision = new Revision();

            var master = new TestTable();
            master.OnRetriveInserts = inserts;
            master.OnRetriveUpdates = updates;
            master.OnRetriveDeletes = deletes;
            ChangeSet changes = master.GetChanges(revision);


            Assert.AreEqual(changes.Inserts, inserts);
            Assert.AreEqual(changes.Updates, updates);
            Assert.AreEqual(changes.Deletes, deletes);
        }
        [Test]
        public void CanUpdateChangeSet()
        {
            var slave = new TestTable();
            var changeSet = new ChangeSet();
            changeSet.Inserts = new[] {new Row(), new Row(), };
            changeSet.Updates = new[] { new Row(), new Row(), };
            changeSet.Deletes = new[] { new Row(), new Row(), };

            slave.Update(changeSet);
           

            Assert.AreEqual(changeSet.Inserts,slave.OnRetriveInserts);
            Assert.AreEqual(changeSet.Updates,slave.OnRetriveUpdates);
            Assert.AreEqual(changeSet.Deletes,slave.OnRetriveDeletes);
        }
    }

    public class Row
    {
    }

    public class TestTable : Table
    {
        public Revision Revision;
        public IEnumerable<Row> OnRetriveInserts;
        public IEnumerable<Row> OnRetriveUpdates;
        public IEnumerable<Row> OnRetriveDeletes;

        protected override Revision OnGetRevision()
        {
            return Revision;
        }

        protected override void DeleteRows(IEnumerable<Row> enumerable)
        {
            OnRetriveDeletes = enumerable; 
        }

        protected override void UpdateRows(IEnumerable<Row> enumerable)
        {
            OnRetriveUpdates = enumerable;
        }

        protected override void InsertRows(IEnumerable<Row> rows)
        {
            OnRetriveInserts = rows;
            
        }

        protected override IEnumerable<Row> RetriveDeletes(Revision revision)
        {
            return OnRetriveDeletes;
        }

        protected override IEnumerable<Row> RetriveUpdates(Revision revision)
        {
            return OnRetriveUpdates;
        }

        protected override IEnumerable<Row> RetriveInserts(Revision revision)
        {
            return OnRetriveInserts;
        }
    }
}