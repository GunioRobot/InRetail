using System;
using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    public class Part
    {
        public string Title { get; set; }
        private IList<Field> _fields=new List<Field>();
        public IList<Field> Fields
        {
            get { return new List<Field>(_fields).AsReadOnly(); }
        }

        public void AddField(Field field)
        {
            _fields.Add(field);
        }
    }

    public class Field {
        public object Label { get; set; }
        public object Value { get; set; }
    }
}