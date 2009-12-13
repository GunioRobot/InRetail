using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using ProductCatalogModel;
using Tests.InRetail.Exploration;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class YahooFinanceFixture
    {
        [Test]
        public void Test()
        {
            IQueryable<ProductDetailViewModel> models = new ObservableQuery<ProductDetailViewModel>(new Provider());
            models.Where(x => x.Description.Contains("a"));
        }


    }

    public class Provider : QueryProvider
    {
        public override string GetQueryText(Expression expression)
        {
            return "";
        }

        public override object Execute(Expression expression)
        {
            return null;
        }
    }


    public class ObservableQuery<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable, IObservable<T>
    {
        private QueryProvider provider;
        private Expression expression;

        public ObservableQuery(QueryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            this.provider = provider;
            expression = Expression.Constant(this);
        }

        public ObservableQuery(QueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            this.provider = provider;
            this.expression = expression;
        }

        Expression IQueryable.Expression
        {
            get { return expression; }
        }

        Type IQueryable.ElementType
        {
            get { return typeof(T); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)provider.Execute(expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IObservable<T>)provider.Execute(this.expression)).ToEnumerable().GetEnumerator();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return ((IObservable<T>)provider.Execute(this.expression)).Subscribe(observer);
        }

        public override string ToString()
        {
            return provider.GetQueryText(expression);
        }

    }
}