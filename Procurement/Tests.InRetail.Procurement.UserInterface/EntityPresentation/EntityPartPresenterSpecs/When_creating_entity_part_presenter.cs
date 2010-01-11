using System.Collections.Generic;
using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;
using Moq;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class When_creating_entity_part_presenter : Specification
    {
        private EntityPartPresenter _entityPartPresenter;


        public override void Given()
        {
            
        }

        public override void When()
        {
            _entityPartPresenter = new EntityPartPresenter();
        }


    }
}