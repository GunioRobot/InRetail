using System.Collections.Generic;
using System.Linq;
using InRetail.EntityPresentation;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartProviderSpecs
{
    public class When_Requesting_EntityParts : Specification
    {
        private EntityPartProvider<PurchaseOrder> provider;
        private IEnumerable<EntityPartPresenter> entityParts;

        public override void Given()
        {
            var order = ObjectMother.Make_Vaild_PurchaseOrder();
            provider = new EntityPartProvider<PurchaseOrder>();
        }

        public override void When()
        {
            entityParts = provider.GetEntityParts();
        }

        [It]
        public void Should_Return_Five_Parts_According_Class_Attributes()
        {
            entityParts.Count().ShouldEqual(5);
        }
    }
}