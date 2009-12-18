using InRetail.Configuration;
using InRetail.Services;
using InRetail.Shell;
using Moq;
using Moq.Language.Flow;
using NServiceBus;
using NUnit.Framework;
using StructureMap;
using InRetail.ProductCatalog;
using Microsoft.Practices.Composite.Regions;

namespace Tests.InRetail.Configuration
{
    [TestFixture]
    public class StructureMapTests
    {
        [Test]
        public void Will_be_able_to_re_create_the_database_schema_in_sqlite()
        {

            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry<ApplicationRegistry>();
                                             x.AddRegistry<ProductCatalogRegistry>();
                                             x.AddRegistry<DomainRegistry>();
                                             x.AddRegistry<ReportingRegistry>();
                                             x.AddRegistry<ServicesRegister>();
                                         });

            var mockRegionManager = new Mock<IRegionManager>();
            var mockRegion = new Mock<IRegion>(MockBehavior.Loose);
            mockRegionManager.Setup(x => x.Regions["mainRegion"]).Returns(mockRegion.Object);
            var mockBus = new Mock<IBus>();

            ObjectFactory.Inject(mockRegionManager.Object);
            ObjectFactory.Inject(mockBus.Object);
            ObjectFactory.AssertConfigurationIsValid();

            ObjectFactory.ResetDefaults();
        }
    }
}