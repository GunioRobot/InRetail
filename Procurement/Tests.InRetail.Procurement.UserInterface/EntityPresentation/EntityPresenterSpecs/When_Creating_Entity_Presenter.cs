using System;
using InRetail.EntityPresentation;
using Moq;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresenterSpecs
{
    public class When_Creating_Entity_Presenter : Specification
    {
        private EntityPresenter<SomeEntity> entityPresenter;
        private EntityPartPresenter[] _entityPartsPresenterPresenter;
        private SomeEntity someEntity;
        private IEntityPartProvider<SomeEntity> partProvider;

        public override void Given()
        {
            partProvider = new Mock<IEntityPartProvider<SomeEntity>>().Object;
            someEntity = new SomeEntity();


            someEntity.Screen_Name_To_Return = "Sreen";
            _entityPartsPresenterPresenter = new[] { new Mock<EntityPartPresenter>().Object };
            partProvider.Setup(x => x.GetEntityParts()).Returns(_entityPartsPresenterPresenter);
        }

        public override void When()
        {
            entityPresenter = new EntityPresenter<SomeEntity>(partProvider, someEntity);
        }

        [It]
        public void Should_Have_EntityParts_Populated()
        {
            entityPresenter.EntityParts.ShouldContainEqualElements(_entityPartsPresenterPresenter);
        }

        [It]
        public void TitleProperty_Should_Have_Value_Provided_By_Entity()
        {
            entityPresenter.Title.ShouldEqual("Sreen");
        }
    }

    public class SomeEntity : EntityBase {
        public string Screen_Name_To_Return;

        public override string GetEntityScreenName()
        {
            return Screen_Name_To_Return;
        }
    }
}