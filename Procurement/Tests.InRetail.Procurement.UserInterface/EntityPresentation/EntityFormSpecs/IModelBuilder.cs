namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs
{
    public interface IModelBuilder
    {
        IPresentationModel Build<T>(T purchaseOrder);
    }
}