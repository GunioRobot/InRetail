using System;
using System.Collections.Generic;
using InRetail.EntityPresentation;
using Moq;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresenterSpecs
{
    public class When_Creating_Entity_Presenter : Specification
    {
        protected EntityPresenter<SomeEntity> entityPresenter;
        protected SomeEntity someEntity;
        protected IEntityView<SomeEntity> entityView;
        protected IList<IPartPresenter> partPresenters;

        public override void Given()
        {
            someEntity = new SomeEntity();
            someEntity.Screen_Name_To_Return = "Sreen";

            entityView = Moq.Mock<IEntityView<SomeEntity>>();

            partPresenters = new[] { Moq.Mock<IPartPresenter>() };
        }

        public override void When()
        {
            entityPresenter = new EntityPresenter<SomeEntity>(entityView, someEntity, partPresenters);
        }

        [It]
        public void Should_Set_View_Property()
        {
            entityPresenter.View.ShouldEqual(entityView);
        }

        [It]
        public void Should_Pass_Parts_To_View()
        {
            entityView.Verify(x => x.Bind(partPresenters[0]));
        }

        [It]
        public void TitleProperty_Should_Have_Value_Provided_By_Entity()
        {
            entityPresenter.Title.ShouldEqual("Sreen");
        }
    }

    public class When_Asking_CanClose_Question_To_Entity_Presenter : When_Creating_Entity_Presenter
    {
        private bool canClose;

        public override void Given()
        {
            base.Given();
            partPresenters = new[] { Moq.Mock<IPartPresenter>(), Moq.Mock<IPartPresenter>(), Moq.Mock<IPartPresenter>() };
            partPresenters[0].Setup(x => x.CanClose()).Returns(true);
            partPresenters[1].Setup(x => x.CanClose()).Returns(false);
            partPresenters[2].Setup(x => x.CanClose()).Returns(true);
        }

        public override void When()
        {
            base.When();
            canClose = entityPresenter.CanClose();
        }

        [It]
        public void Should_Return_False()
        {
            canClose.ShouldBeFalse();
        }

        [It]
        public void Should_Ask_Each_PartPresenter_Same_Question()
        {
            partPresenters[0].Verify(x => x.CanClose(),Times.Exactly(1));
            partPresenters[1].Verify(x => x.CanClose(), Times.Exactly(1));
            partPresenters[2].Verify(x => x.CanClose(), Times.Never());
        }
    }

    public class SomeEntity : EntityBase
    {
        public string Screen_Name_To_Return;

        public override string GetEntityScreenName()
        {
            return Screen_Name_To_Return;
        }
    }
}