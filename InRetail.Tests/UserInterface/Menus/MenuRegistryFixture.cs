using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.Shell.Menus;
using InRetail.UiCore.Menus;
using NUnit.Framework;

namespace Tests.InRetail.UserInterface.Menus
{
    [TestFixture]
    public class When_registering_actions_to_MenuRegistry : BaseTestFixture<MenuRegistry>
    {
        protected override void When()
        {
            IMenuRegistry prod_cat_menu = SubjectUnderTest
                .Register("Product Catalog")
                .ToMenu();

            IMenuRegistry sub_menu = prod_cat_menu
                .Register("Sub Menu")
                .ToMenu();

            prod_cat_menu
                .Register("Add Product")
                .ToScreen<TestScreen>();

            prod_cat_menu
                .Register("Add Product Wizard")
                .ToWizard<TestWizard>();

            sub_menu
                .Register("sub Add Product")
                .ToScreen<TestScreen>();
        }

        [Then]
        public void Then_MenuRegistry_Should_Has_Menus_Structure()
        {
            var prod_cat_menu = (SgMenuItem)SubjectUnderTest.Menus.First();
            prod_cat_menu.Name.WillBe("Product Catalog");
            
            var sub_menu = (SgMenuItem)prod_cat_menu.Menus.First();
            sub_menu.Name.WillBe("Sub Menu");
            sub_menu.Menus.First().Name.WillBe("sub Add Product");

            prod_cat_menu.Menus.First(x => x.Name == "Add Product");
            prod_cat_menu.Menus.First(x => x.Name == "Add Product Wizard");
        }
    }
 
    public class TestWizard
    {
    }

    public class TestScreen
    {
    }
}