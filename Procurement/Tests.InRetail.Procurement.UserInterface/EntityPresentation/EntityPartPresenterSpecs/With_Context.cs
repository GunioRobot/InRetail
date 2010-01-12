using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.EntityPresentation;
using Moq;

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