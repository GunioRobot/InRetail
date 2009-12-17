using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using InRetail.ProductCatalog.QueryModel;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace InRetail.ProductCatalog.Persistence
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            Configuration cfg = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("database.db"))
                .Mappings(m => m.AutoMappings
                    .Add(AutoMap
                    .AssemblyOf<ProductDetailViewModel>()
                    .Conventions
                    .Add(new ClassConvention())))
                
                
                .BuildConfiguration();
            cfg.SetProperty(Environment.ProxyFactoryFactoryClass, typeof (ProxyFactoryFactory).AssemblyQualifiedName);
            cfg.SetProperty(Environment.ConnectionDriver, typeof (SQLite20Driver).AssemblyQualifiedName);


            var export = new SchemaExport(cfg); 
            export.Drop(true,true);
            export.Create(true,true);


            ISessionFactory sessionFactory = cfg.BuildSessionFactory();

            ForRequestedType<Configuration>().AsSingletons()
                .TheDefault.IsThis(cfg);

            ForRequestedType<ISessionFactory>().AsSingletons()
                .TheDefault.IsThis(sessionFactory);

            ForRequestedType<ISession>().CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());

            ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.Hybrid)
                .TheDefaultIsConcreteType<UnitOfWork>();

            ForRequestedType<IDatabaseBuilder>()
                .TheDefaultIsConcreteType<DatabaseBuilder>();
        }
    }
}