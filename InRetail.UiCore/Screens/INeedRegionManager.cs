using Microsoft.Practices.Composite.Regions;

namespace InRetail.UiCore.Screens
{
    public interface INeedRegionManager : IScreen
    {
        void Initialize(IRegionManager regionManager);
    }
}