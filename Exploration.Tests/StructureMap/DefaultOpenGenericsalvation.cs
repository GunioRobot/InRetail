using System;
using StructureMap;
using Xunit;
using StructureMap.Configuration.DSL;


namespace Exploration.Tests.StructureMap
{
    public class DefaultOpenGenericRequest
    {
        private IContainer container;

        public DefaultOpenGenericRequest()
        {

            ObjectFactory.Configure(x => x.AddRegistry<TestViewRegistry>());
            container = ObjectFactory.Container;

        }

        [Fact]
        public void RetriveOpenType()
        {

            var defView = container.GetInstance<IView<Bar>>();
            var fooView = container.GetInstance<IView<Foo>>();

            Assert.True(defView is DefaultView<Bar>);
            Assert.True(fooView is FooView);
        }

        [Fact]
        public void Resolving_closing_type_by_type_parameter_type()
        {
            var generic = typeof (IView<>);
            System.Type specificType = generic.MakeGenericType(new System.Type[] { typeof(Foo) });
            var o = container.GetInstance(specificType);

            Assert.True(o is FooView);
        }
    }

    
    
    
    public class TestViewRegistry : Registry
    {
        public TestViewRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<TestViewRegistry>();
                x.ConnectImplementationsToTypesClosing(typeof(IView<>));
            });
            
            
            ForRequestedType(typeof(IView<>)).TheDefaultIsConcreteType(typeof(DefaultView<>));
        }
    }



    public interface IView<T>
    {

    }

    public class DefaultView<T> : IView<T>
    {

    }

    public class FooView : IView<Foo>
    {

    }



    public class Foo
    {

    }

    public class Bar
    {

    }
}