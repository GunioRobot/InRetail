using System;
using NHibernate;

namespace InRetail.ProductCatalog.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISession CurrentSession { get; }
        void Commit();
        void Rollback();
    }
}