using System;
using System.Collections.Generic;

namespace InRetail.EntityPresentation
{
    public interface IPart
    {
        IEnumerable<IMessageMap> MessageMaps { get; }
    }

    public interface IMessageMap
    {
        string Name { get; set; }
        IMessageView BuildMessageView();
    }
    public interface IMessageView { }

}