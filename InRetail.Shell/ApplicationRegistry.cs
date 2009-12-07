using InRetail.UiCore;
using InRetail.UiCore.Actions;
using Microsoft.Practices.Composite.Regions;
using StructureMap.Configuration.DSL;

namespace InRetail.Shell
{
    public class ApplicationRegistry:Registry
    {
        public ApplicationRegistry()
        {
            ForRequestedType<IRegion>()
                .AddInstances(x => x.ConstructedBy((c) => c.GetInstance<IRegionManager>().Regions["mainRegion"]).WithName("mainRegion"));

            ForSingletonOf<IScreenCollection>().TheDefault.Is.OfConcreteType<ScreenCollection>()
                .CtorDependency<IRegion>("mainRegion")
                .Is(x => x.TheInstanceNamed("mainRegion"));

            ForSingletonOf<IScreenConductor>().TheDefaultIsConcreteType<ScreenConductor>();
            ForSingletonOf<IScreenFactory>().TheDefaultIsConcreteType<ScreenFactory>();
            ForSingletonOf<IScreenObjectRegistry>().TheDefaultIsConcreteType<ScreenObjectRegistry>();


            ForSingletonOf<IShellView>().TheDefaultIsConcreteType<Window1>();
          
        }
    }
}