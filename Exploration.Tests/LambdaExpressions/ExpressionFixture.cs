using System;
using System.Linq.Expressions;
using System.Reflection;
using Tests.InRetail.Exploration;
using Xunit;

namespace Exploration.Tests.LambdaExpressions
{
    public class ExpressionFixture
    {
        [Fact]
        public void MemberAccess()
        {
            Expression<Func<Foo, string>> expression1 = x => x.PropStr;

            var expression = expression1.Body as MemberExpression;
            var member = expression.Member as PropertyInfo;
            
            var foo = new Foo() {PropStr = "Acho"};
            

            
            var func = expression1.Compile();

            string actual = func(foo);

            Assert.Equal("Acho", actual);
        }
    }
    public class Foo
    {
        public string PropStr { get; set; }
    }

}