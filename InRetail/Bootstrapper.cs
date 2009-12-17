using InRetail.Shell;
using NServiceBus;
using StructureMap;

namespace InRetail
{
    public class Bootstrapper
    {
        private Bootstrapper()
        { }

        public static void Bootstrap()
        {
            BootstrapStructureMap();
            BootstrapNServiceBus();
        }

        private static void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<ApplicationRegistry>());
        }

        private static void BootstrapNServiceBus()
        {
            Configure.With()
                .StructureMapBuilder(ObjectFactory.Container)
                .MsmqSubscriptionStorage()
                .XmlSerializer()
                .MsmqTransport()
                    .IsTransactional(true)
                    .PurgeOnStartup(false)
                .UnicastBus()
                    .ImpersonateSender(false)
                    .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}