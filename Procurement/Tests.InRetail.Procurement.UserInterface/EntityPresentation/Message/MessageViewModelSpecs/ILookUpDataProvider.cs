using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public interface ILookUpDataProvider {
        IEnumerable<T> GetLookupData<T>();
    }
}