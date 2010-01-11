using System;
using InRetail.UiCore.Screens;
using Microsoft.Practices.Composite.Regions;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenCollectionSpecs
{
    public class When_Adding_Screen_Witch_Needs_Local_Scope_For_RegionManager : With_Empty_Screens
    {
        private INeedRegionManager needRegionManagerScreen;
        protected IRegionManager localRegionManager;
        

        public override void Given()
        {
            needRegionManagerScreen = Moq.Mock<INeedRegionManager>();

            localRegionManager = Moq.Mock<IRegionManager>();
            mainRegion.Setup(x => x.Add(It.IsAny<object>(), It.IsAny<string>(), true))
                .Returns(localRegionManager);
        }

        public override void When()
        {
            screenCollection.Add(needRegionManagerScreen);
        }

        [It]
        public void Then_Should_Create_Local_Scoped_RegionManager()
        {
            mainRegion.Verify(x => x.Add(It.IsAny<object>(), It.IsAny<string>(), true),Times.Once());

            mainRegion.Verify(x => x.Add(It.IsAny<object>(), It.IsAny<string>(), false),Times.Never());
            mainRegion.Verify(x => x.Add(It.IsAny<object>()), Times.Never());
        }

        [It]
        public void Then_Should_Give_Local_Scoped_RegionManager_To_Screen()
        {
            needRegionManagerScreen.Verify(x=>x.Initialize(localRegionManager));
        }
    }

    
}