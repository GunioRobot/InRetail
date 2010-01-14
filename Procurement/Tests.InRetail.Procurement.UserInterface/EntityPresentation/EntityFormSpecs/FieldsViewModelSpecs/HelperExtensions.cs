using System;
using System.Collections.Generic;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.FieldsViewModelSpecs
{
    public static class HelperExtensions
    {
        public static Action ChangesValue(this IField_v2 f, object value)
        {
            var mock = f.Moq();
            var valueFunction = new Subject<object>();
            Action a = () =>
                           {
                               valueFunction.OnNext(value);
                               mock.SetupGet(x => x.Value).Returns(value);
                           };

            mock.SetupGet(x => x.ObservableValue).Returns(valueFunction);
            return a;
        }
    }
}