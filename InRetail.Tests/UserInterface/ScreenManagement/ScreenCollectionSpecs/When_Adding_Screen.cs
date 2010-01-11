using System;
using InRetail.UiCore.Screens;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenCollectionSpecs
{
    public class When_Adding_Screen : With_Empty_Screens
    {
        private IScreen screen;

        public override void Given()
        {
            screen = Moq.Mock<IScreen>();
            screen.Moq().SetupGet(x => x.View).Returns(new object());
        }

        public override void When()
        {
            screenCollection.Add(screen);
        }

        [It]
        public void Should_Add_Screen_In_To_MainRegion()
        {
            mainRegion.Verify(x => x.Add(screen.View),Times.Once());
        }
    }
}