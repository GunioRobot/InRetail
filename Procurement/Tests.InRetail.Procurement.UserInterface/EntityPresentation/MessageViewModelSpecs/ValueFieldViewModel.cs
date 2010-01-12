using System;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class ValueFieldViewModel<T> : FieldViewModelBase<T>
    {
        public ValueFieldViewModel(IField_v2<T> field)
            : base(field)
        { }


    }
}