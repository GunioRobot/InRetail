using InRetail.UiCore;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public class When_Opening_Screen_Witch_Is_Already_Opened_But_It_Is_Not_Active : With_Open_Screens
    {
        protected TestScreen3 createdScreen;

        public override void Given()
        {
            base.Given();
            createdScreen = new Mock<TestScreen3>().Object;
            screenFactory.Moq().Setup(x => x.Build<TestScreen3>()).Returns(createdScreen);
        }

        public override void When()
        {
            conductor.OpenScreen(new SingletonScreenSubject<TestScreen3>());
        }

        [It]
        public void Then_Screen_Should_Not_Created_By_Screen_Factory()
        {
            screenFactory.Moq().Verify(x => x.Build<TestScreen3>(), Times.Never());
        }

        [It]
        public void Then_Screen_Should_Activated()
        {
            notActiveScreen.Moq().Verify(x => x.Activate(screenObjectRegistry), Times.Once());
        }

        [It]
        public void Then_Screen_Should_Not_Added_To_ScreenCollection()
        {
            screenCollection.Moq().Verify(x => x.Add(createdScreen), Times.Never());
        }

        [It]
        public void Then_Screen_Should_Shown_By_Screen_Collection()
        {
            screenCollection.Moq().Verify(x => x.Show(notActiveScreen), Times.Once());
        }
    }
}