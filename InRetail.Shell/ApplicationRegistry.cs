using InRetail.Shell.Actions;
using InRetail.Shell.Dialogs;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Dialogs;
using InRetail.UiCore.Menus;
using InRetail.UiCore.Screens;
using Microsoft.Practices.Composite.Regions;
using StructureMap.Configuration.DSL;

namespace InRetail.Shell
{
    public class ApplicationRegistry:Registry
    {
        public ApplicationRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.ConnectImplementationsToTypesClosing(typeof (IScreen<>));
                     });
            For(typeof(IScreenSubject<>)).Use(typeof(ScreenSubject<>));

            ForRequestedType<IRegion>()
                .AddInstances(x => x.ConstructedBy((c) =>
                                                       {
                                                           var instance = c.GetInstance<IRegionManager>();
                                                           IRegion region = instance.Regions["mainRegion"];
                                                           return                                          region;
                                                       }
                                       ).WithName("mainRegion"));
            

            ForSingletonOf<IScreenCollection>().TheDefault.Is.OfConcreteType<ScreenCollection>()
                .CtorDependency<IRegion>("mainRegion")
                .Is(x => x.TheInstanceNamed("mainRegion"));

            ForSingletonOf<IScreenConductor>().TheDefaultIsConcreteType<ScreenConductor>();
            ForSingletonOf<IScreenFactory>().TheDefaultIsConcreteType<ScreenFactory>();
            ForSingletonOf<IScreenObjectRegistry>().TheDefaultIsConcreteType<ScreenObjectRegistry>();
            ForSingletonOf<IMenuRegistry>().TheDefaultIsConcreteType<MenuRegistry>();
            
            

            ForSingletonOf<IShellView>().TheDefaultIsConcreteType<ShellView>();
            ForRequestedType<IDialogLauncher>().TheDefaultIsConcreteType<DialogLauncher>();
        }
    }
}