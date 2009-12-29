using InRetail.Shell.Menus;
using InRetail.UiCore.Menus;
using NUnit.Framework;

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
            
            var sub_menu= prod_cat_menu[2].As<IMenuContainer>();
            
            sub_menu.Name.WillBe("Sub Menu");
            sub_menu[0].Name.WillBe("sub Add Product");
        }
    }
}