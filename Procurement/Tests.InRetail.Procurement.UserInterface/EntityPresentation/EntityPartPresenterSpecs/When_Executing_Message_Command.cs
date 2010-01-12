using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.AssertHelpers;
using Moq;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_Executing_Message_Command : When_Creating_Part_Presenter
    {
        protected IMessageView m1MessageView;
        protected IMessageView m2MessageView;

        public override void Given()
        {
            m1MessageView = Moq.Mock<IMessageView>();
            m2MessageView = Moq.Mock<IMessageView>();
            part = New.Part().WithMessageMaps(x => x.WithName("m1").CanBuildMessageView(() => m1MessageView),
                                              x => x.WithName("m2").CanBuildMessageView(() => m2MessageView)).Build();
        }

        public override void When()
        {
            base.When();
            partPresenter.MessageCommands[0].Command.Execute(null);
        }

        [It]
        public void Should_Switch_To_MessageEditingView()
        {
            partView.Verify(x => x.SwitchToEditMode(m1MessageView), Times.Once());
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