using System;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    internal abstract class FieldBase<TEntity> : IField_v2
    {
        public abstract string Label { get; }
        public abstract object Value { get; }
        public abstract IObservable<object> ObservableValue { get; }
        public abstract void Build(TEntity target);
    }
}