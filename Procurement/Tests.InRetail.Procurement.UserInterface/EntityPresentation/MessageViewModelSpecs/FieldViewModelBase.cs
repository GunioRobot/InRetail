namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public abstract class FieldViewModelBase<T> : IFieldViewModel
    {
        protected FieldViewModelBase(IField_v2<T> field)
        {
            Label = field.Label;
            NewValue = field.Value;
            OldValue = field.Value;
        }

        public string Label { get; set; }
        public T NewValue { get; set; }
        public T OldValue { get; set; }

    }
}