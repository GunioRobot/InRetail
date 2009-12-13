using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProductCatalogModel;


namespace InRetail.ProductCatalog.Presenters
{
    public class TestFixture
    {
        public void Test()
        {
            var cont = new ProductCatalogContainer(new Uri(@"http://localhost:2691/ProductCatalog.svc/"));
            var @descending = cont.ProductDetailViewModels.OrderByDescending(x => x.Description).Take(3);
            foreach (var list in @descending.ToList())
            {
                Console.WriteLine(list.Description);
            }
        }
        public void Test2()
        {
            var cont = new ProductCatalogContainer(new Uri(@"http://localhost:2691/ProductCatalog.svc/"));
            var infos = cont.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).
                Where(
                    x=>x.PropertyType.IsGenericType && 
                    x.PropertyType.GetGenericArguments().FirstOrDefault(t => t == typeof(ProductDetailViewModel)) != null).
                First();
        }
    }
}