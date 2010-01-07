using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InRetail.Procurement.UserInterface;
using InRetail.ProductCatalog;
using InRetail.Shell.StructureMap;
using InRetail.UiCore;
using InRetail.UiCore.Extensions;
using Microsoft.Practices.Composite.Modularity;
using StructureMap;
using System.Threading;

namespace InRetail.Shell
{
    public class ShellBootstrapper : StructureMapBootstrapper
    {
        public static void BootStrap()
        {
            ObjectFactory.Configure(x => x.AddRegistry<ApplicationRegistry>());
            var bootstrapper = new ShellBootstrapper();
            bootstrapper.Run();
            bootstrapper.StartupShell();
        }

    
        protected override IContainer CreateContainer()
        {
            return ObjectFactory.GetInstance<IContainer>();
        }

        protected override DependencyObject CreateShell()
        {
            string have = Container.WhatDoIHave();
            var presenter = Container.GetInstance<ShellPresenter>();
            IShellView view = presenter.View;
            view.ShowView();
            Container.Configure(x => x.ForSingletonOf<Window>().TheDefault.IsThis(view.As<Window>()));
            return view as DependencyObject;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog()
                .AddModule(typeof(ProductCatalogModule))
                .AddModule(typeof(ProcurementModule));
        }

        private void StartupShell()
        {
            List<IStartable> startables = Container.Model.PluginTypes
                .Where(p => p.Implements<IStartable>())
                .Select(x => x.To<IStartable>(Container)).ToList();
            startables.Each(x => x.Start());
            Container.Model.PluginTypes
                .Where(p => p.Implements<INeedBuildUp>())
                .Select(x => x.To<INeedBuildUp>(Container))
                .Each(Container.BuildUp);
        }
    }
}