using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exploration.Tests.Patterns
{
    public class VisitorFixture
    {
        private List<IElement> _elements;

        public VisitorFixture()
        {
            _elements = new List<IElement>
                            {
                                new LeafElement(),
                                new CompositeElement()
                                    {
                                        new LeafElement(),
                                        new CompositeElement()
                                            {
                                                new LeafElement(),
                                                new LeafElement(),
                                                new LeafElement(),
                                                new CompositeElement()
                                                    {
                                                        new LeafElement(),
                                                        new CompositeElement()
                                                            {
                                                                new LeafElement(),
                                                                new LeafElement(),
                                                                new LeafElement(),
                                                                new LeafElement(),
                                                                new LeafElement(),
                                                            },
                                                    },
                                                new LeafElement().Add(new ValueField()).Add(new ValueField()).Add(new RefField()),
                                                new LeafElement(),
                                                new LeafElement(),
                                            },
                                    },
                                new LeafElement(),

                            };



        }
        [Fact]
        public void BuildPresenters()
        {
            var presentersBuilderVisitor = new PresentersBuilderVisitor();
            presentersBuilderVisitor.Build(_elements);
        }
    }

    internal abstract class ElementVisitor
    {
        public abstract void VisitLeafElement(LeafElement leafElement);

        public abstract void VisitCompositeElement(CompositeElement compositeElement);

    }

    class PresentersBuilderVisitor : ElementVisitor
    {
        private string ident = "";
        public override void VisitLeafElement(LeafElement leafElement)
        {
            Console.WriteLine(ident + "Build: " + leafElement);
            int i = -1;
            leafElement.Fields.Do(_ => i++).Run(x => Console.WriteLine(ident + "\t\t\t has fields[" + i + "]: " + x));
        }

        public override void VisitCompositeElement(CompositeElement compositeElement)
        {
            Console.WriteLine(ident + "Build: " + compositeElement);
            var oldIdent = ident;
            ident += "   ";
            compositeElement.Run(x => x.Accept(this));
            ident = oldIdent;
        }

        public void Build(List<IElement> elements)
        {
            elements.Run(x => x.Accept(this));
        }
    }

    internal interface IElement
    {
        void Accept(ElementVisitor visitor);
    }

    class LeafElement : IElement
    {
        readonly IList<IEntityField> _fields = new List<IEntityField>();
        public IList<IEntityField> Fields
        {
            get { return _fields; }
        }

        public void Accept(ElementVisitor visitor)
        {
            visitor.VisitLeafElement(this);
        }

        public LeafElement Add(IEntityField entityField)
        {
            Fields.Add(entityField);
            return this;
        }
    }

    class CompositeElement : List<IElement>, IElement
    {
        public void Accept(ElementVisitor visitor)
        {
            visitor.VisitCompositeElement(this);
        }
    }

    internal interface IEntityField { }

    class CollectionField : IEntityField { }

    class RefField : IEntityField { }

    class ValueField : IEntityField { }
}