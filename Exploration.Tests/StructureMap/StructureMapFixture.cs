using StructureMap;
using Xunit;
namespace Exploration.Tests.StructureMap
{
    public class StructureMapFixture
    {
        [Fact]
        public void Resolve_Open_Generic_Type()
        {
            ObjectFactory.Configure(c=>
                                        {
                                            c.For(typeof (IOpenFoo<>)).Use(typeof (OpenFoo<>));
                                        });
            var instance = ObjectFactory.GetInstance<IOpenFoo<TestClass>>();
            Assert.NotNull(instance);
        }

      
    }

    public class TestClass
    {
    }

    public class OpenFoo<T> : IOpenFoo<T>
    {
    }

    public interface IOpenFoo<T>
    {
    }
}