using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NServiceBus;

namespace Tests.InRetail.Procurement.UserInterface
{
    public interface IModelToMessageMap<TModel> where TModel : IModel
    {
        IEnumerable<string> MappedProperties { get; }
        IMessage BuildMessage(TModel model);
    }

    public abstract class ModelToMessageMap<TModel, TMessage> : IModelToMessageMap<TModel>
        where TModel : IModel
        where TMessage : IMessage, new()
    {
        private readonly List<string> _mappedProperties = new List<string>();
        private readonly List<Action<TMessage, TModel>> _msgPropSetActions = new List<Action<TMessage, TModel>>();

        public IMessage BuildMessage(TModel model)
        {
            var message = new TMessage();
            _msgPropSetActions.Run(x => x(message, model));
            return message;
        }

        public virtual IEnumerable<string> MappedProperties
        {
            get { return _mappedProperties; }
        }

        #region Mapping DSL Impl

        protected IMapExpression<TProperty> Map<TProperty>(Expression<Func<TMessage, TProperty>> expression)
        {
            var propertyInfo = (PropertyInfo) ((MemberExpression) expression.Body).Member;
            return new MapExpression<TProperty>(this, (message, value) => propertyInfo.SetValue(message, value, null));
        }


        public interface IMapExpression<TProperty>
        {
            void To(Expression<Func<TModel, TProperty>> expression);
        }

        private class MapExpression<TProperty> : IMapExpression<TProperty>
        {
            private readonly ModelToMessageMap<TModel, TMessage> _modelToMessageMap;
            private readonly Action<TMessage, TProperty> _setMessageValueAction;

            internal MapExpression(ModelToMessageMap<TModel, TMessage> modelToMessageMap,
                                   Action<TMessage, TProperty> setMessagePropertyAction)
            {
                _modelToMessageMap = modelToMessageMap;
                _setMessageValueAction = setMessagePropertyAction;
            }

            public void To(Expression<Func<TModel, TProperty>> expression)
            {
                var getModelPropertyValueFunc = expression.Compile();
                _modelToMessageMap._msgPropSetActions.Add(
                    (message, model) => _setMessageValueAction(message, getModelPropertyValueFunc(model)));

                _modelToMessageMap._mappedProperties.Add(((MemberExpression) expression.Body).Member.Name);
            }
        }

        #endregion
    }
}