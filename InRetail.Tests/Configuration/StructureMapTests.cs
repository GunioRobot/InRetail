using InRetail.Configuration;
using InRetail.Services;
using NUnit.Framework;
using StructureMap;

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
                                             x.AddRegistry<DomainRegistry>();
                                             x.AddRegistry<ReportingRegistry>();
                                             x.AddRegistry<ServicesRegister>();
                                         });

            ObjectFactory.AssertConfigurationIsValid();

            ObjectFactory.ResetDefaults();
        }
    }
}