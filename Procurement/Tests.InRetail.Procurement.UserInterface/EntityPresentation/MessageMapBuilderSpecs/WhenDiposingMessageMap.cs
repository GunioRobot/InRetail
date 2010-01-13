using System;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    public class When_Clearing_Reference_To_MessageMap : When_Changing_Entity_Properties
    {
        public override void When()
        {
            base.When();
        }

        [It]
        public void Should_be_Garbage_Collectible()
        {
            var wr = new WeakReference(messageMap);
            wr.IsAlive.ShouldBeTrue();
            
            messageMap = null;

            GC.Collect();

            wr.IsAlive.ShouldBeFalse();

        }
    }

}