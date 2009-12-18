using System;
using System.Collections.Generic;
using System.Linq;
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

    public interface IMenuItem
    {
        string Name { get; }
    }

    public abstract class MenuRegistryBase : IMenuRegistry
    {
        private IList<IMenuItem> _menus;

        protected MenuRegistryBase()
        {
            _menus = new List<IMenuItem>();
        }

        public IEnumerable<IMenuItem> Menus
        {
            get { return _menus; }
            
        }
        public IMenuExpression Register(string menuTitle)
        {
            return new MenuExpression(this, menuTitle);
        }
        public void AddMenu(IMenuItem item)
        {
            _menus.Add(item);
        }
    }

    public class MenuRegistry : MenuRegistryBase
    {
        public MenuRegistry() : base()
        {
        }
    }
    public class SgMenuAction : IMenuItem
    {
        private string _title;

        public string Name
        {
            get { return _title; }
            set { _title = value; }
        }
    }
    public class SgMenuItem :MenuRegistryBase, IMenuItem
    {
        private string _title;

        public string Name
        {
            get { return _title; }
            set { _title = value; }
        }
    }

    class MenuExpression : IMenuExpression
    {
        private readonly IMenuRegistry _registry;
        private readonly string _title;

        public MenuExpression(IMenuRegistry registry, string title)
        {
            _registry = registry;
            _title = title;
        }

        public IMenuRegistry ToMenu()
        {
            var item = new SgMenuItem() { Name = _title };
            _registry.AddMenu(item);
            return item;
        }

        public void ToScreen<T>()
        {
            var item = new SgMenuAction() { Name = _title };
            _registry.AddMenu(item);
        }

        public void ToWizard<T>()
        {
            var item = new SgMenuAction() { Name = _title };
            _registry.AddMenu(item);
        }
    }

    public interface IMenuRegistry
    {
        IMenuExpression Register(string s);
        void AddMenu(IMenuItem item);
    }

    public interface IMenuExpression
    {
        IMenuRegistry ToMenu();
        void ToScreen<T>();
        void ToWizard<T>();
    }

    public class TestWizard
    {
    }

    public class TestScreen
    {
    }
}