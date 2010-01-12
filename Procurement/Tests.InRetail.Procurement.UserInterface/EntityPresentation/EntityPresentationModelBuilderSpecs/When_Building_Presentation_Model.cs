using System;
using InRetail.EntityPresentation;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    public class When_Building_Presentation_Model : Specification
    {
        public override void Given()
        {
            new SomeEntity();
            new EntityPresentationModelBuilder();
        }
    }


    public class SomeEntity : IEntity
    {
        public string GetEntityScreenName()
        {
            throw new NotImplementedException();
        }
    }

    public class EntityPresentationModelBuilder { }
}