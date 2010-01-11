using System.Collections.Generic;

namespace InRetail.EntityPresentation
{
    public interface IEntityPartProvider<T>
    {
        IEnumerable<IPart> GetEntityParts();
    }
}