using System;
using System.Collections.Generic;
using System.Linq;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    public class Part
    {
        public string Title { get; set; }
        
        private IList<MessageMap> _messageMaps = new List<MessageMap>();

        public IList<IField_v2> Fields
        {
            get { return new List<IField_v2>(_messageMaps.SelectMany(x => x.Fields)).AsReadOnly(); }
        }

        public IList<MessageMap> MessageMaps
        {
            get { return new List<MessageMap>(_messageMaps).AsReadOnly(); }

        }

        public void AddMessageMap(MessageMap messageMap)
        {
            _messageMaps.Add(messageMap);
        }
    }

    public class MessageMap {
        private readonly IList<IField_v2> _fields = new List<IField_v2>();
        private readonly string _title;

        public MessageMap(string title)
        {
            _title = title;
        }

        public string Title
        {
            get { return _title; }
        }

        public IList<IField_v2> Fields
        {
            get { return new List<IField_v2>(_fields).AsReadOnly(); }
        }

        public MessageMap AddField(IField_v2 field)
        {
            _fields.Add(field);
            return this;
        }
    }

    public class Field : IField_v2
    {
        public string Label { get; set; }

        public IObservable<object> ObservableValue
        {
            get { throw new NotImplementedException(); }
        }

        public IFieldViewModel BuildViewModel()
        {
            throw new NotImplementedException();
        }

        public object Value { get; set; }
    }
}