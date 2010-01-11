using System;
using System.Linq;
using Tests.InRetail.Procurement.AssertHelpers;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_Sending_Message : When_Executing_Message_Command
    {
        public override void Given()
        {
            base.Given();
        }

        public override void When()
        {
            base.When();
            partPresenter.SendCommand.Execute(null);
        }

        [It]
        public void Should_Switch_View()
        {
            partView.Verify(x => x.SwitchToViewMode());
        }

        [It]
        public void Should_Disable_SendCommand()
        {
            partPresenter.SendCommand.ShouldBeDisabled();
        }

        [It]
        public void Should_Disable_CancelCommand()
        {
            partPresenter.CancelCommand.ShouldBeDisabled();
        }
    }
}