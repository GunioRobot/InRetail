using System;
using NUnit.Framework;
using System.Linq.Expressions;
namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class ExpressionFixture
    {
        public void RetriveMemberInfoFromExpression()
        {
            var subject = new TestSubject();

            Expression<Func<TestSubject,object>> expres = (x) => x.SomeProperty;
            Expression<Action<TestSubject>> expres1 = (x) => x.Method();
            Expression<Action<TestSubject>> expres2 = (x) => x.VoidMethodWithParams(1,"");
            Expression<Func<TestSubject, object>> expres3 = (x) => x.ReturnMethodWithParams(1, "");

        }
    }

    public class TestSubject
    {
        public object SomeProperty { get; set; }

        public void Method()
        {
            
        }

        public void VoidMethodWithParams(int i, string empty)
        {
            
        }

        public Object ReturnMethodWithParams(int i, string empty)
        {
            throw new NotImplementedException();
        }
    }
}