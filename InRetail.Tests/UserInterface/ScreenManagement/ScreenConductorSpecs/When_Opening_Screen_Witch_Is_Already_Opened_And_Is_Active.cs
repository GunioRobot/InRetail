using InRetail.UiCore;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public class When_Opening_Screen_Witch_Is_Already_Opened_And_It_Is_Active : With_Open_Screens
    {
        protected TestScreen2 screenToOpen;

        public override void Given()
        {
            base.Given();
            screenToOpen = new Mock<TestScreen2>().Object;
            screenFactory.Moq().Setup(x => x.Build<TestScreen2>()).Returns(screenToOpen);
        }

        public override void When()
        {
            conductor.OpenScreen(new SingletonScreenSubject<TestScreen2>());
        }

        [It]
        public void Then_Screen_Should_Not_Created_By_Screen_Factory()
        {
            screenFactory.Moq().Verify(x => x.Build<TestScreen2>(), Times.Never());
        }

        [It]
        public void Then_Screen_Should_Not_Activated()
        {
            screenToOpen.Moq().Verify(x => x.Activate(screenObjectRegistry), Times.Never());
        }

        [It]
        public void Then_Screen_Should_Not_Added_To_ScreenCollection()
        {
            screenCollection.Moq().Verify(x => x.Add(screenToOpen), Times.Never());
        }

        [It]
        public void Then_Screen_Should_Not_Shown_By_Screen_Collection()
        {
            screenCollection.Moq().Verify(x => x.Show(screenToOpen), Times.Never());
        }
    }
}