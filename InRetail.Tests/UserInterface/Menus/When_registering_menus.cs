using System;
using InRetail.Shell;
using InRetail.Shell.Menus;
using InRetail.UiCore;
using InRetail.UiCore.Menus;
using InRetail.UiCore.Screens;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace Tests.InRetail.UserInterface.Menus
{
    [TestFixture]
    public class When_registering_menus : BaseTestFixture<MenuRegistry>
    {
        protected override void When()
        {
            var prod_cat_menu = SubjectUnderTest.Register("Product Catalog").ToContainer();
            prod_cat_menu.Register("Add Product").ToScreen<TestScreen>();
            prod_cat_menu.Register("Add Product Wizard").ToWizard<TestWizard>();
            var sub_menu = prod_cat_menu.Register("Sub Menu").ToContainer();
            sub_menu.Register("sub Add Product").ToScreen<TestScreen>();
        }

        [Then]
        public void Menu_Registry_Should_Have_Menu_Structure()
        {
            var prod_cat_menu = SubjectUnderTest[0].As<IMenuContainer>();

            prod_cat_menu.Name.WillBe("Product Catalog");
            prod_cat_menu[0].Name.WillBe("Add Product");
            prod_cat_menu[1].Name.WillBe("Add Product Wizard");

            var sub_menu = prod_cat_menu[2].As<IMenuContainer>();

            sub_menu.Name.WillBe("Sub Menu");
            sub_menu[0].Name.WillBe("sub Add Product");
        }
    }

    [TestFixture]
    public class When_Executing_to_screen_menu_action_command : BaseTestFixture<MenuRegistry>
    {
        private IScreenConductor conductor;
        

        protected override void Given()
        {
            conductor = new Mock<IScreenConductor>().Object;
            ObjectFactory.Container.Inject(conductor);
            SubjectUnderTest.Register("Add Product").ToScreen<TestScreen>();
        }

        protected override void When()
        {
            var menuAction = SubjectUnderTest[0].As<IMenuAction>();
            menuAction.Command.Execute(null);
        }

        [Then]
        public void Should_Call_ScreenConductor_To_Open_Screen()
        {
            conductor.Moq().Verify(x => x.OpenScreen(new SingletonScreenSubject<TestScreen>()));
        }
    }
}