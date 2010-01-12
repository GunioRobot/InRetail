using System;
using System.Collections.Generic;

namespace InRetail.EntityPresentation
{
    public interface IPart
    {
        IEnumerable<IMessageMap> MessageMaps { get; }
        IEnumerable<IField> Fields { get;  }
    }

    public interface IField {
        IFieldView BuildFieldView();
    }

    public interface IMessageMap
    {
        string Title { get; set; }
        IEnumerable<IField> Fields { get; set; }


        //old staff
        string Name { get; }
        

        IMessageView BuildMessageView();
    }
    public interface IMessageView { }

}