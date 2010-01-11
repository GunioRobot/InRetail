using System;
using InRetail.Shell;
using Microsoft.Practices.Composite.Regions;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenCollectionSpecs
{
    public abstract class With_Empty_Screens:Specification
    {
        protected IRegion mainRegion;
        
        protected ScreenCollection screenCollection;

        protected With_Empty_Screens()
        {
            
            mainRegion = Moq.Mock<IRegion>();
            screenCollection = new ScreenCollection(mainRegion);
        }
    }
}