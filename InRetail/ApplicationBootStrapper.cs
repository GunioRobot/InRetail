using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InRetail.Configuration;
using InRetail.Services;

using StructureMap;


namespace InRetail
{
    public class ApplicationBootStrapper
    {
        public void BootStrapTheApplication()
        {
            DomainDatabaseBootStrapper.BootStrap();
            ReportingDatabaseBootStrapper.BootStrap();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ApplicationRegistry>();
                x.AddRegistry<DomainRegistry>();
                x.AddRegistry<ReportingRegistry>();
                x.AddRegistry<ServicesRegister>();
            });
            ObjectFactory.AssertConfigurationIsValid();

            RegisterCommandHandlersInMessageRouter.BootStrap();
            RegisterEventHandlersInMessageRouter.BootStrap();
        }

        public static void BootStrap()
        {
            new ApplicationBootStrapper().BootStrapTheApplication();
            StartupShell(ObjectFactory.GetInstance<IContainer>());
        }

        private static void StartupShell(IContainer container)
        {
            container.GetInstance<SystemActions>().Register();

            // Find all the possible services in the main application
            // IoC Container that implement an "IStartable" interface
            List<IStartable> startables = container.Model.PluginTypes
                .Where(p => p.IsStartable())
                .Select(x => x.ToStartable(container)).ToList();

            // Tell each "IStartable" to Start()
            ListExtensions.Each(startables, x => x.Start());

            // Build up
            container.Model.PluginTypes
                .Where(p => p.Implements<INeedBuildUp>())
                .Select(x => x.To<INeedBuildUp>(container))
                .Each(container.BuildUp);
        }

        public static Window BootStrapShell()
        {
            BootStrap();
            return ObjectFactory.GetInstance<Window>();
        }
    }
}