using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    public abstract class EntityToMessageMap<TEntity, TMessage> : IEntityMap<TEntity>
    {
        IList<FieldBase<TEntity>> _fields = new List<FieldBase<TEntity>>();
        public MessageMap Build(TEntity order)
        {
            var messageMap = new MessageMap("a");
            _fields.Do(f => f.Build(order)).Run(f => messageMap.AddField(f));
            return messageMap;
        }

        protected IMapExpression<TProperty> Map<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            return new MapExpression<TProperty>(this, expression);
        }

        public interface IMapExpression<TProperty>
        {
            ILabelExpression To(Expression<Func<TMessage, TProperty>> expression);
        }

        public interface ILabelExpression
        {
            void Label(string label);
        }

        internal class MapExpression<TProperty> : IMapExpression<TProperty>, ILabelExpression
        {
            private readonly EntityToMessageMap<TEntity, TMessage> _map;
            private readonly Expression<Func<TEntity, TProperty>> _entityExpr;
            private Expression<Func<TMessage, TProperty>> _mesgExpr;

            public MapExpression(EntityToMessageMap<TEntity, TMessage> map, Expression<Func<TEntity, TProperty>> entityExpr)
            {
                _map = map;
                _entityExpr = entityExpr;
            }

            public ILabelExpression To(Expression<Func<TMessage, TProperty>> expression)
            {
                _mesgExpr = expression;
                return this;
            }

            public void Label(string label)
            {
                var valueField = new ValueField<TEntity, TProperty>(label, _entityExpr);
                _map._fields.Add(valueField);
            }
        }

    }
}