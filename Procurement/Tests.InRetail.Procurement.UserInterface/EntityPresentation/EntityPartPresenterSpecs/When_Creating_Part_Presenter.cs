using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_Creating_Part_Presenter : With_Context
    {
        public override void When()
        {
            partPresenter = new EntityPartPresenter(part, partView);
        }

        [It]
        public void Should_Have_Message_Commands()
        {
            partPresenter.MessageCommands[0].Name.ShouldEqual("SomeMessage Name");
        }

        [It]
        public void SendCommand_Should_Be_Disabled()
        {
            partPresenter.SendCommand.ShouldBeDisabled();
        }

        [It]
        public void CancelCommand_Should_Be_Disabled()
        {
            partPresenter.CancelCommand.ShouldBeDisabled();
        }
    }
}