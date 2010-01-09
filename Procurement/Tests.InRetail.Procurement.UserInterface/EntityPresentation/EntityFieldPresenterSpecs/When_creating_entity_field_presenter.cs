using System;
using System.Linq;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFieldPresenterSpecs
{
    public class When_creating_entity_field_presenter : Specification
    {
        private EntityFieldPresenter<string> fieldPresenter;
        protected SomeEntity entity;
        protected IObservable<string> obsEntityProp;

        public override void Given()
        {
            entity = new SomeEntity() { StrProp = "A" };
            obsEntityProp = entity.FromPropertyChanged(x => x.StrProp).StartWith(entity.StrProp);
        }

        public override void When()
        {
            fieldPresenter = new EntityFieldPresenter<string>("Label", obsEntityProp);
        }

        [It]
        public void Should_Notify_Title_Property_Changes()
        {
            fieldPresenter.ShouldBeNotifyProperty(x => x.Title, "some value");
        }

        [It]
        public void Should_Notify_Value_Property_Changes()
        {
            fieldPresenter.ShouldBeNotifyProperty(x => x.Value, "some value");
        }

        [It]
        public void Should_Have_TitleProperty_Value()
        {
            fieldPresenter.Title.ShouldEqual("Label");
        }

        [It]
        public void Should_Observe_Assosiated_Entity_Property_Value_Changes()
        {
            fieldPresenter.Value.ShouldEqual("A");

            entity.StrProp = "Acho";

            fieldPresenter.Value.ShouldEqual("Acho");
        }

    }

    public class SomeEntity : EntityBase
    {
        private string _strProp;
        public string StrProp
        {
            get { return _strProp; }
            set { _strProp = value; NotifyPropertyChanged(this.Property(x => x.StrProp)); }
        }
    }
}