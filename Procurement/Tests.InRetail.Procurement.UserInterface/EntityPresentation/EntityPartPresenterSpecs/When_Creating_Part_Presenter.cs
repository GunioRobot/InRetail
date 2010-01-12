using System;
using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_Creating_Part_Presenter : With_Context
    {
        private IFieldView fieldView1;
        private IFieldView fieldView2;

        public override void Given()
        {
            part = New.Part()
                .WithMessageMaps(x => x.WithName("SomeMessage Name1"),
                                 x => x.WithName("SomeMessage Name2"),
                                 x => x.WithName("SomeMessage Name3"))
                .WithFields(x => x.CanBuildFieldView(() => fieldView1),
                            x => x.CanBuildFieldView(() => fieldView2)).Build();
        }

        public override void When()
        {
            partPresenter = new EntityPartPresenter(part, partView);
        }

        [It]
        public void Should_Have_Message_Commands()
        {
            partPresenter.MessageCommands[0].Name.ShouldEqual("SomeMessage Name1");
            partPresenter.MessageCommands[0].Command.ShouldNotBeNull();
            partPresenter.MessageCommands[1].Name.ShouldEqual("SomeMessage Name2");
            partPresenter.MessageCommands[1].Command.ShouldNotBeNull();
            partPresenter.MessageCommands[2].Name.ShouldEqual("SomeMessage Name3");
            partPresenter.MessageCommands[2].Command.ShouldNotBeNull();
        }

        [It]
        public void Should_Have_Field_Views()
        {
            partPresenter.FieldViews[0].ShouldEqual(fieldView1);
            partPresenter.FieldViews[1].ShouldEqual(fieldView2);
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