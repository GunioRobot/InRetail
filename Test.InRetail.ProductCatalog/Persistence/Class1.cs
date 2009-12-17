using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InRetail.ProductCatalog.Persistence;
using NUnit.Framework;

namespace Test.InRetail.ProductCatalog.Persistence
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void CanInstantiateRegistry()
        {

            var registry = new NHibernateRegistry();
        }
    }
}