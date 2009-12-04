using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using InRetail.Shell.StructureMap;
using InRetail.UiCore;
using InRetail.UiCore.Extensions;
using StructureMap;

namespace InRetail.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new InRetailBootstrapper();
            bootstrapper.Run();
            
            StartupShell(bootstrapper.Container);
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static void StartupShell(IContainer container)
        {
            //container.GetInstance<SystemActions>().Register();

            // Find all the possible services in the main application
            // IoC Container that implement an "IStartable" interface
            List<IStartable> startables = container.Model.PluginTypes
                .Where(p => p.Implements<IStartable>())
                .Select(x => x.To<IStartable>(container)).ToList();

            // Tell each "IStartable" to Start()
            startables.Each(x => x.Start());

            // Build up
            container.Model.PluginTypes
                .Where(p => p.Implements<INeedBuildUp>())
                .Select(x => x.To<INeedBuildUp>(container))
                .Each(container.BuildUp);
        }
    }
}