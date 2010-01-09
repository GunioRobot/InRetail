using System.Collections.Generic;

namespace InRetail.EntityPresentation
{
    public interface IEntityPartProvider<T>
    {
        IEnumerable<EntityPartPresenter> GetEntityParts();
    }
}