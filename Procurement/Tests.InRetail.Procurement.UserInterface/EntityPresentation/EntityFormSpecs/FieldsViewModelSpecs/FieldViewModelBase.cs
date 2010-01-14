using System;
using System.ComponentModel;
using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.FieldsViewModelSpecs
{
    public abstract class FieldViewModelBase:INotifyPropertyChanged {
        private IFieldView _view;
        private readonly IField_v2 _field;

        protected FieldViewModelBase(IFieldView view, IField_v2 field)
        {
            _view = view;
            _field = field;
            _field.ObservableValue.Subscribe(_ => this.Property(x => x.Value));
        }

        public IFieldView View
        {
            get { return _view; }
        }

        public string Label
        {
            get { return _field.Label; }
        }
        public object Value
        {
            get { return _field.Value;
                 }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e=new PropertyChangedEventArgs(propertyName);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}