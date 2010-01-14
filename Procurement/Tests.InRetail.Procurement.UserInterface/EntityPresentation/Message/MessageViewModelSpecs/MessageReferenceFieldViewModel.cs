using System.Collections.Generic;
using System.Linq;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class MessageReferenceFieldViewModel<T> : MessageFieldViewModelBase
    {
        private readonly ILookUpDataProvider _lookUpDataProvider;

        public MessageReferenceFieldViewModel(IField_v2 field, ILookUpDataProvider lookUpDataProvider)
            : base(field)
        {
            LookUpData = new List<T>();
            _lookUpDataProvider = lookUpDataProvider;
            _lookUpDataProvider.GetLookupData<T>().Run(LookUpData.Add);
        }

        public IList<T> LookUpData { get; private set; }
    }
}