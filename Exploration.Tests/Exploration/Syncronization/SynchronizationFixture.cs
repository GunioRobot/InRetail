using NUnit.Framework;
using Moq;
namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class SynchronizationFixture
    {
        [Test]
        public void SyncronizeStagesTest()
        {
            var revision = new Revision();
            var changeSet = new ChangeSet();

            var mockMaster = new Mock<ITable>();
            var mockSlave = new Mock<ITable>();

            mockMaster.Setup(x => x.GetChanges(revision)).Returns(changeSet);
            mockSlave.Setup(x => x.GetRevision()).Returns(revision);

            var syncronizator = new Syncronizator();

            syncronizator.Master = mockMaster.Object;
            syncronizator.Slave = mockSlave.Object;

            syncronizator.Syncronize();

            mockSlave.Verify(x=>x.GetRevision(),Times.Once());
            mockMaster.Verify(x => x.GetChanges(revision), Times.Once());
            mockSlave.Verify(x => x.Update(Moq.It.IsAny<ChangeSet>()), Times.Once());

        }
    }


}