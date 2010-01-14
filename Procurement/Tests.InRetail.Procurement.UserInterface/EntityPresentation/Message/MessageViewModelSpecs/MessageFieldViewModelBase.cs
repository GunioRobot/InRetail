namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public abstract class MessageFieldViewModelBase : IFieldViewModel
    {
        protected MessageFieldViewModelBase(IField_v2 field)
        {
            Label = field.Label;
            NewValue = field.Value;
            OldValue = field.Value;
        }

        public string Label { get; set; }
        public object NewValue { get; set; }
        public object OldValue { get; set; }

    }
}