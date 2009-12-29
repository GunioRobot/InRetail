using InRetail.UiCore;
using Moq;

namespace Tests.InRetail.UserInterface.ApplicationShell
{
    public class When_Opening_Screen_Using_Subject : With_Emty_Conductor
    {
        protected TestScreen2 testScreen;

        public override void Given()
        {
            base.Given();
            testScreen = new Mock<TestScreen2>().Object;
            screenFactory.Moq().Setup(x => x.Build<TestScreen2>()).Returns(testScreen);
        }

        public override void When()
        {
            conductor.OpenScreen(new SingletonScreenSubject<TestScreen2>());
        }

        [It]
        public void Then_Screen_Should_Be_Created_By_Screen_Factory()
        {
            screenFactory.Moq().Verify(x => x.Build<TestScreen2>(), Times.Once());
        }

        [It]
        public void Then_Screen_Should_Activated_And_Given_Chance_To_Register_ScreenActions()
        {
            testScreen.Moq().Verify(x => x.Activate(screenObjectRegistry), Times.Once());
        }

        [It]
        public void Then_Screen_Should_Added_to_Screen_Collection()
        {
            screenCollection.Moq().Verify(x => x.Add(testScreen), Times.Once());
        }

        [It]
        public void Then_Screen_Should_Shown_By_Screen_Collection()
        {
            screenCollection.Moq().Verify(x => x.Show(testScreen), Times.Once());
        }
    }
}