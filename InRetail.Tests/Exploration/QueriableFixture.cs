using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using ProductCatalogModel;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class QueriableFixture
    {
        [Test]
        public void QueriableTest()
        {
            var context = new ProductCatalogContainer(new Uri("http://localhost:2691/ProductCatalog.svc/"));
            var queryable = context.ProductDetailViewModels.Where(x => x.Description.StartsWith("a")).Take(100);
            var observable = queryable.ToObservable();
            var loop = SynchronizationContexts.ReactiveEventLoop;

            var observable2 = Observable
                .Create<ProductDetailViewModel>((o) =>
                                                    {
                                                        foreach (var p in context.ProductDetailViewModels.Take(100))
                                                        {
                                                            o.OnNext(p);
                                                        }
                                                        o.OnCompleted();
                                                        return null;
                                                    });
            var b = true;

            observable.Subscribe(
                v =>
                    {
                        Debug.WriteLine(v.Description + Thread.CurrentThread.Name);
                        Thread.Sleep(20);
                    },
                _ => { b = false; });

            Thread.Sleep(5000);
        }
    }

    public class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>,
                            IOrderedQueryable
    {
        private QueryProvider provider;
        private Expression expression;

        public Query(QueryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            this.provider = provider;
            expression = Expression.Constant(this);
        }

        public Query(QueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof (IQueryable<T>).IsAssignableFrom(expression.Type))
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
            get { return typeof (T); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) provider.Execute(expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) provider.Execute(expression)).GetEnumerator();
        }

        public override string ToString()
        {
            return provider.GetQueryText(expression);
        }
    }

    public abstract class QueryProvider : IQueryProvider
    {
        protected QueryProvider()
        {
        }

        IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression)
        {
            return new Query<S>(this, expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return
                    (IQueryable)
                    Activator.CreateInstance(typeof (Query<>).MakeGenericType(elementType),
                                             new object[] {this, expression});
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        S IQueryProvider.Execute<S>(Expression expression)
        {
            return (S) Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return Execute(expression);
        }

        public abstract string GetQueryText(Expression expression);

        public abstract object Execute(Expression expression);
    }

    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof (string))
                return null;
            if (seqType.IsArray)
                return typeof (IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof (IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }

            if (seqType.BaseType != null && seqType.BaseType != typeof (object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }
    }



}