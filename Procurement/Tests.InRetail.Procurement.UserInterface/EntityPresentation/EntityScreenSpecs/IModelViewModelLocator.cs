namespace Tests.InRetail.Procurement.EntityPresentation.EntityScreenSpecs
{
    public interface IModelViewModelLocator {
        IPresentationViewModel BuildViewModel<T>(T entity);
    }
}