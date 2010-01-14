using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.FieldsViewModelSpecs
{
    public class TestableFieldViewModelBase : FieldViewModelBase
    {
        public TestableFieldViewModelBase(IFieldView view, IField_v2 field) : base(view, field) { }
    }
}