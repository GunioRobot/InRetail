using System.Collections.Generic;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;
using Moq;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_creating_entity_part_presenter : Specification
    {
        private EntityPartPresenter partPresenter;
        private IEnumerable<IEntityFieldPresenter> fieldPresenters;
        private IEnumerable<IEntityMessagePresenter> messagePresenters;

        public override void Given()
        {
            fieldPresenters = new[] { new Mock<IEntityFieldPresenter>().Object };
            messagePresenters = new[] { new Mock<IEntityMessagePresenter>().Object };
        }

        public override void When()
        {
            partPresenter = new EntityPartPresenter("Title", fieldPresenters, messagePresenters);
        }

        [It]
        public void Should_Have_Title_Property_Value()
        {
            partPresenter.Title.ShouldEqual("Title");
        }

        [It]
        public void Should_Have_EntityFields_Collection_Populated()
        {
            partPresenter.EntityFields.ShouldContainEqualElements(fieldPresenters);
        }

        [It]
        public void Should_Have_EntityMessages_Collection_Populated()
        {
            partPresenter.EntityMessages.ShouldContainEqualElements(messagePresenters);
        }
    }
}