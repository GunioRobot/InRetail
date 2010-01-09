using System.Collections.Generic;


namespace Tests.InRetail.Procurement.EntityPresentation
{
    public interface IEntityPartProvider<T>
    {
        IEnumerable<EntityPartPresenter> GetEntityParts();
    }
}