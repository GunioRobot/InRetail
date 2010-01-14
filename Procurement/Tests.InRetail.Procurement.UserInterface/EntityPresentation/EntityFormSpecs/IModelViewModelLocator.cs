namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs
{
    public interface IModelViewModelLocator {
        IPresentationViewModel BuildViewModel<T>(T entity);
    }
}