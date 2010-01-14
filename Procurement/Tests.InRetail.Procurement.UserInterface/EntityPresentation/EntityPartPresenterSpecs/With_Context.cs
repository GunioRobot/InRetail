using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.EntityPresentation;
using Moq;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class With_Context : Specification
    {
        protected IEntityPartView partView;
        protected IPart part;
        protected EntityPartPresenter partPresenter;

        public With_Context()
        {
            partView = Moq.Mock<IEntityPartView>();
        }
    }

    internal class New
    {
        public static PartBuilder Part()
        {
            return new PartBuilder();
        }

        public static MessageMap_v2Builder MessageMap_v2(string title)
        {
            return new MessageMap_v2Builder(title);
        }
    }

    internal class MessageMap_v2Builder : IBuilder<IMessageMap_v2>
    {
        private readonly string _title;
        private IList<IField_v2> _fields;

        public MessageMap_v2Builder(string title)
        {
            _title = title;
        }

        public MessageMap_v2Builder Fields(params Action<Field_v2Builder>[] configs)
        {
            _fields=new List<IField_v2>();
            foreach (var action in configs)
            {
                var bilder = new Field_v2Builder();
                action(bilder);
                _fields.Add(bilder.Build());
            }
            return this;
        }

        public IMessageMap_v2 Build()
        {
            var mock = new Mock<IMessageMap_v2>();
            mock.SetupGet(x => x.Title).Returns(_title);
            mock.Setup(x => x.Fields).Returns(_fields);
            return mock.Object;
        }
    }

    internal class Field_v2Builder : IBuilder<IField_v2> {
        private string _label;
        private object _value;

        public Field_v2Builder Label(string label)
        {
            _label = label;
            return this;
        }

        public Field_v2Builder Value<T>(T value)
        {
            _value = value;
            return this;
        }

        public IField_v2 Build()
        {
            var mock = new Mock<IField_v2>();
            mock.SetupGet(x => x.Label).Returns(_label);
            mock.SetupGet(x => x.Value).Returns(_value);
            return mock.Object;
        }
    }

    internal class PartBuilder:IBuilder<IPart>
    {
        private IList<IMessageMap> _messageMaps;
        private IList<IField> _fields;

        public PartBuilder WithMessageMaps(params Action<MessageMapBulder>[] messageMaps)
        {
            _messageMaps=new List<IMessageMap>();
            foreach (var messageMap in messageMaps)
            {
                var bulder = new MessageMapBulder();
                messageMap(bulder);
                _messageMaps.Add(bulder.Build());
            }
            return this;
        }

        public PartBuilder WithFields(params Action<FieldBuilder>[] fields)
        {
            _fields=new List<IField>();
            foreach (var field in fields)
            {
                var bulder = new FieldBuilder();
                field(bulder);
                _fields.Add(bulder.Build());
            }
            return this;
        } 

        public IPart Build()
        {
            var part = new Mock<IPart>();
            if (_messageMaps != null) part.SetupGet(x => x.MessageMaps).Returns(_messageMaps);
            if (_fields != null) part.SetupGet(x => x.Fields).Returns(_fields);
            return part.Object;
        }
    }

    internal class FieldBuilder:IBuilder<IField> {
        private Func<IFieldView> _fieldViewFactor;

        public IField Build()
        {
            var mock = new Mock<IField>();
            if (_fieldViewFactor != null) mock.Setup(x => x.BuildFieldView()).Returns(_fieldViewFactor);
            return mock.Object;
        }

        public FieldBuilder CanBuildFieldView(Func<IFieldView> func)
        {
            _fieldViewFactor = func;
            return this;
        }
    }

    internal class MessageMapBulder : IBuilder<IMessageMap>
    {
        private string _name;
        private Func<IMessageView> _viewFactory;

        public MessageMapBulder WithName(string name)
        {
            _name = name;
            return this;
        }

        public MessageMapBulder CanBuildMessageView(Func<IMessageView> viewFactory)
        {
            _viewFactory = viewFactory;
            return this;
        }   
        
        public IMessageMap Build()
        {
            var mock = new Mock<IMessageMap>();
            mock.Setup(x => x.Name).Returns(_name);
            mock.Setup(x => x.BuildMessageView()).Returns(_viewFactory);
            return mock.Object;
        }
    }

    internal interface IBuilder<T> 
    {
        T Build();
    }
}