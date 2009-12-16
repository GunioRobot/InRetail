using InRetail.Configuration;
using InRetail.Services;
using InRetail.Shell;
using Moq;
using Moq.Language.Flow;
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

            var mock = new Mock<IRegionManager>();
            var mock1 = new Mock<IRegion>(MockBehavior.Loose);
            mock.Setup(x => x.Regions["mainRegion"]).Returns(mock1.Object);

            ObjectFactory.Inject<IRegionManager>(mock.Object);
            ObjectFactory.AssertConfigurationIsValid();

            ObjectFactory.ResetDefaults();
        }
    }
}