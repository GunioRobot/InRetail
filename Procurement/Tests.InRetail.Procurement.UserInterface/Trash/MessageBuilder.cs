using System.Collections.Generic;
using System.Linq;
using NServiceBus;

namespace Tests.InRetail.Procurement
{
    public class MessageBuilder<TModel> where TModel : IModel
    {
        private readonly IList<string> _changedProps= new List<string>();
        private readonly TModel _model;
        private readonly IList<IModelToMessageMap<TModel>> maps = new List<IModelToMessageMap<TModel>>();

        public MessageBuilder(TModel model)
        {
            _model = model;
            _model.PropertyChanged += (s, e) => {
                                                    var x = e.PropertyName;
                                                    if (!_changedProps.Contains(x))
                                                        _changedProps.Add(x);};
                
        }

        public void Add<TMessage>(ModelToMessageMap<TModel, TMessage> map) where TMessage : IMessage, new()
        {
            maps.Add(map);
        }

        public IEnumerable<IMessage> GetMessages()
        {
            IEnumerable<IMessage> messages = maps
                .Where(x => x.MappedProperties.Intersect(_changedProps).Count() > 0)
                .Select(x => x.BuildMessage(_model));
            return messages;
        }
    }
}