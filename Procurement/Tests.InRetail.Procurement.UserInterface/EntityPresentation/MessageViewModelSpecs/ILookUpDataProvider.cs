using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public interface ILookUpDataProvider {
        IEnumerable<Supplier> GetSuppliers();
    }
}