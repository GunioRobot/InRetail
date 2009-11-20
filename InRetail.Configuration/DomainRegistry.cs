using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using InRetail.Bus;
using InRetail.Bus.Direct;
using InRetail.EventStore;
using InRetail.EventStore.SQLite;
using InRetail.EventStore.Storage;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using IUnitOfWork=InRetail.EventStore.IUnitOfWork;

namespace InRetail.Configuration
{
    public class DomainRegistry : Registry
    {
        private const string sqLiteConnectionString = "Data Source=domainDataBase.db3";

        public DomainRegistry()
        {
            ForRequestedType<IBus>()
                .CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<DirectBus>();

            ForRequestedType<IRouteMessages>()
                .AsSingletons()
                .TheDefault.Is.OfConcreteType<MessageRouter>();

            ForRequestedType<IFormatter>()
                .TheDefault.Is.ConstructedBy(x => new BinaryFormatter());

            ForRequestedType<IDomainEventStorage>()
                .TheDefault.Is.OfConcreteType<DomainEventStorage>()
                .WithCtorArg("sqLiteConnectionString").EqualTo(sqLiteConnectionString);

            ForRequestedType<IIdentityMap>()
                .TheDefault.Is.OfConcreteType<EventStoreIdentityMap>();

            ForRequestedType<IEventStoreUnitOfWork>()
                .CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<EventStoreUnitOfWork>();

            ForRequestedType<IUnitOfWork>()
                .TheDefault.Is.ConstructedBy(x => x.GetInstance<IEventStoreUnitOfWork>());

            ForRequestedType<IDomainRepository>()
                .TheDefault.Is.OfConcreteType<DomainRepository>();
        }
    }
}