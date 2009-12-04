using NUnit.Framework;
using System.Windows.Input;

namespace Tests.InRetail.UserInterface.Actions
{
    [TestFixture]
    public class Screen_action_dsl_tests : BaseTestFixture<ScreenActionsRegistry>
    {
        protected override void When()
        {

            //SubjectUnderTest
            //    .MenuAction("Product Catalog")
            //        .Add("Products")


            //SubjectUnderTest
            //    .MenuAction("Products Catalog").Bind(Key.F1).ToScreen<TestScreen>()
            //        .AddSubActions(x=>x.MenuAction("Products").Bind(Key.F2).ToScreen<TestScreen>()
            //                                .AddSubActions(x=>x.MenuAction("My Products").Bind(Key.F2).ToScreen<TestScreen>(),
            //                                               x=>x.MenuAction("All Products").Bind(Key.F2).ToScreen<TestScreen>()),
            //                       x=>x.MenuAction("Configuration").Bind(Key.None).ToScreen<TestScreen>()
            //                           );

            //        .MenuAction("Products").Bind(Key.F2).ToScreen<TestScreen>().Composite()
            //            .MenuAction("My Products").Bind(Key.F2).ToScreen<TestScreen>()
            //            .MenuAction("All Products").Bind(Key.F2).ToScreen<TestScreen>();
        }

        [Then]
        public void Test()
        {
        }
    }

    public class TestScreen
    {
    }

    public class ScreenActionsRegistry
    {
    }
}