using InRetail.Shell.Actions;
using InRetail.Shell.Dialogs;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Dialogs;
using Microsoft.Practices.Composite.Regions;
using StructureMap.Configuration.DSL;

namespace InRetail.Shell
{
    public class ApplicationRegistry:Registry
    {
        public ApplicationRegistry()
        {
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
            
            

            ForSingletonOf<IShellView>().TheDefaultIsConcreteType<Window1>();
            ForRequestedType<IDialogLauncher>().TheDefaultIsConcreteType<DialogLauncher>();
        }
    }
}