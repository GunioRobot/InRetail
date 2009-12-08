using System;
using NUnit.Framework;
using System.Windows.Input;

namespace Tests.InRetail.UserInterface.Actions
{
    [TestFixture]
    public class Screen_action_dsl_tests : BaseTestFixture<ScreenActionsRegistry>
    {
        private MenuActionsRegistry registry;

        protected override void Given()
        {
            registry = new MenuActionsRegistry();
        }

        protected override void When()
        {
           // registry.Register("Products").ToScreen<IQueriable<>>();
        }

        [Then]
        public void Test()
        {
        }
    }

    public class MenuActionsRegistry
    {
    }

    public class TestScreen
    {
    }

    public class ScreenActionsRegistry
    {
    }
}