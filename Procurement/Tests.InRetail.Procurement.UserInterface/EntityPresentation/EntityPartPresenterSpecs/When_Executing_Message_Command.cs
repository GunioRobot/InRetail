using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.AssertHelpers;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_Executing_Message_Command : When_Creating_Part_Presenter
    {
        protected IMessageView messageView;

        public override void Given()
        {
            base.Given();
            messageView = Moq.Mock<IMessageView>();
            messageMaps[0].Setup(x => x.BuildMessageView()).Returns(messageView);
        }

        public override void When()
        {
            base.When();
            partPresenter.MessageCommands[0].Command.Execute(null);
        }

        [It]
        public void Should_Switch_To_MessageEditingView()
        {
            partView.Verify(x => x.SwitchToEditMode(messageView));
        }

        [It]
        public void Should_Activate_SendCommand()
        {
            partPresenter.SendCommand.ShouldBeEnabled();
        }

        [It]
        public void Should_Activate_CancelCommand()
        {
            partPresenter.CancelCommand.ShouldBeEnabled();
        }
    }
}