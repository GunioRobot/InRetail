using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InRetail.ProductCatalog.Services;

namespace InRetail.ProductCatalog.Presenters
{
    public interface IRepository<T> where T:class 
    {
        IQueryable<T> Models { get; }
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private ProductCatalogContainer _container;

        public Repository()
        {
            _container = new ProductCatalogContainer(new Uri(@"http://localhost:2691/ProductCatalog.svc/"));
        }

        public IQueryable<T> Models
        {
            get { return FindSet(); }
        }

        //todo: make Cache for fast access
        private IQueryable<T> FindSet()
        {
            PropertyInfo first = _container.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).
                Where(
                x => x.PropertyType.IsGenericType &&
                     x.PropertyType.GetGenericArguments().FirstOrDefault(t => t == typeof(ProductDetailViewModel)) != null).
                First();
            return (IQueryable<T>)first.GetValue(_container, null);
        }
    }

    public class RepositoryFixture
    {
        public void Test()
        {
            var repository = new Repository<ProductDetailViewModel>();
            var models1 = repository.Models.ToList();
            var models2 = repository.Models.OrderByDescending(x=>x.Description).Take(1).ToList();
        }
    }


}