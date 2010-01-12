using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public interface IMessageMap_v2 {
        string Title { get; }
        IEnumerable<IField_v2> Fields { get; }
    }
}