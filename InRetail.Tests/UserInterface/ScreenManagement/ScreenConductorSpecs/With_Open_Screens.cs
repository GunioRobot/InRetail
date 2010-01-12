using System.Collections.Generic;
using InRetail.UiCore.Screens;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public abstract class With_Open_Screens : With_Emty_Conductor
    {
        protected TestScreen2 activeScreen;
        protected TestScreen3 notActiveScreen;
        protected List<IScreen> OpenedScreens;

        public override void Given()
        {
            base.Given();
            activeScreen = new Mock<TestScreen2>().Object;
            notActiveScreen = new Mock<TestScreen3>().Object;
            OpenedScreens = new List<IScreen> {activeScreen, notActiveScreen};

            screenCollection.Moq().Setup(x => x.Active).Returns(activeScreen);
            screenCollection.Moq().Setup(x => x.AllScreens).Returns(OpenedScreens);
        }
    }
}