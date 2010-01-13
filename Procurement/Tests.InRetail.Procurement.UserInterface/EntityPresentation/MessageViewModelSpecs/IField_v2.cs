using System;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public interface IField_v2
    {
        string Label { get; }
        object Value { get; }
        IObservable<object> ObservableValue { get; }
    }

    public interface IField_v2<T> : IField_v2
    {
        new T Value { get; }
        new IObservable<T> ObservableValue { get; }
    }
}