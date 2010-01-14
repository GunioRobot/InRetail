namespace Tests.InRetail.Procurement.EntityPresentation.EntityScreenSpecs
{
    public interface IModelBuilder
    {
        IPresentationModel Build<T>(T purchaseOrder);
    }
}