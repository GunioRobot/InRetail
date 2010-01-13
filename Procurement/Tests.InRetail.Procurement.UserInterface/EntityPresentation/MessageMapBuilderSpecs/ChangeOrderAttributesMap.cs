using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    public class ChangeOrderAttributesMap : EntityToMessageMap<PurchaseOrder, ChangeOrderAttributes>
    {
        public ChangeOrderAttributesMap()
        {
            Map(x => x.Ref).To(x => x.Ref).Label("Ref");
            Map(x => x.Date).To(x => x.Date).Label("Date");
        }
    }
}