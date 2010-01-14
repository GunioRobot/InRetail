using InRetail.UiCore;
using InRetail.UiCore.Screens;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public class When_Opening_Screen_Witch_Is_Already_Opened_And_It_Is_Active : When_Creating_ScreenConductor
    {
        private IScreen screenToOpen;
        private IScreenSubject screenSubject;

        public override void Given()
        {
            base.Given();

            var activeScreen = screenToOpen = new Mock<IScreen>().Object;
            
            screenSubject = Moq.Mock<IScreenSubject>();
            screenSubject.Setup(x => x.Matches(activeScreen)).Returns(true);

            screenCollection.SetupGet(x => x.Active).Returns(activeScreen);
        }

        public override void When()
        {
            base.When();
            conductor.OpenScreen(screenSubject);
        }

        [It]
        public void Should_Not_Call_ScreenSubject_To_CreateScreen()
        {
            screenFactory.Verify(x => x.Build(It.IsAny<IScreen>()), Times.Never());
        }

        [It]
        public void Should_Not_Activate_Screen()
        {
            screenToOpen.Verify(x => x.Activate(screenObjectRegistry), Times.Never());
        }

        [It]
        public void Should_Not_Add_Screen_To_Screen_Collection()
        {
            screenCollection.Verify(x => x.Add(screenToOpen), Times.Never());
        }

        [It]
        public void Should_Not_Call_ScreenCollection_To_Show_Screen()
        {
            screenCollection.Verify(x => x.Show(screenToOpen), Times.Never());
        }
    }
}