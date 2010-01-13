using System;
using System.Linq;
using System.Linq.Expressions;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    internal class ValueField<TEntity, TProperty> : FieldBase<TEntity>, IField_v2<TProperty>
    {
        private string _label;
        private readonly Expression<Func<TEntity, TProperty>> _property;
        private IObservable<TProperty> _observableValue;
        private IObservable<object> _observableValue2;
        private Func<TEntity, TProperty> _compile;
        private TEntity _target;

        public ValueField(string label, Expression<Func<TEntity, TProperty>> property)
        {
            _label = label;
            _property = property;
        }

        public override void Build(TEntity target)
        {
            _compile = _property.Compile();
            _target = target;
            _compile(target);
            _observableValue = target.FromPropertyChanged(_property);
            _observableValue2 = _observableValue.Select(x => (object)x);
        }

        #region IField_v2<TProperty> Members

        public override string Label
        {
            get { return _label; }
        }
        public override object Value
        {
            get { return _compile(_target); }
        }
        public override IObservable<object> ObservableValue
        {
            get { return _observableValue2; }
        }

        TProperty IField_v2<TProperty>.Value
        {
            get { { return _compile(_target); } }
        }

        IObservable<TProperty> IField_v2<TProperty>.ObservableValue
        {
            get { { return _observableValue; } }
        }
        #endregion

        #region IField_v2 Members

        string IField_v2.Label
        {
            get { return _label; }
        }

        object IField_v2.Value
        {
            get { return _compile(_target); }
        }
        IObservable<object> IField_v2.ObservableValue
        {
            get { return _observableValue2; }
        }

        #endregion
    }
}