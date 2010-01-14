using Tests.InRetail.Procurement.AssertHelpers;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.MessageMapViewModelSpecs
{
    public class When_User_Clicks_Edit_Message_Button : Specification
    {
        private IMessageSender messageSender;
        private MessageMapViewModel viewModel;

        public override void Given()
        {
            messageSender = Moq.Mock<IMessageSender>();

            var messageMap = Moq.Mock<IMessageMap_v2>();
            messageMap.SetupGet(x => x.MessageType).Returns(typeof(ChangeOrderAttributes));

            viewModel = new MessageMapViewModel(messageSender, messageMap);
        }
        
        public override void When()
        {
            viewModel.SendCommand.Click();
        }

        [It]
        public void Should_Call_SendMessage_On_MessageSender()
        {
            messageSender.Verify(x => x.SendMessage(typeof(ChangeOrderAttributes)));
        }
    }
}