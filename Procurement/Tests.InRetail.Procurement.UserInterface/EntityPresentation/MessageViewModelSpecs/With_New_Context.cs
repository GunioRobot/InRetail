namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class With_New_Context:Specification
    {
        protected MessageViewModel viewModel;
        protected IMessageMap_v2 messageMap;
        
        public override void Given()
        {
            messageMap = Moq.Mock<IMessageMap_v2>();
        }

        public override void When()
        {
            viewModel = new MessageViewModel(messageMap);
        }
    }
}