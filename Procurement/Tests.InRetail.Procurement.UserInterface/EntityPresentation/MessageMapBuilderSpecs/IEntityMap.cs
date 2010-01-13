using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    public interface IEntityMap<TEntity>
    {
        MessageMap Build(TEntity entity);
    }
}