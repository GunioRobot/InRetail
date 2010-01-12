using System;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public interface IField_v2
    {
        string Label { get; }
        IFieldViewModel BuildViewModel();
    }

    public interface IField_v2<T> : IField_v2
    {
        T Value { get; }
    }
}