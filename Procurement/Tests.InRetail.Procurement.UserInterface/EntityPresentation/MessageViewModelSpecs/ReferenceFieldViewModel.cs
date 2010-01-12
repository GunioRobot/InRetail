using System.Collections.Generic;
using System.Linq;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class ReferenceFieldViewModel<T> : FieldViewModelBase<T>
    {
        private readonly ILookUpDataProvider _lookUpDataProvider;

        public ReferenceFieldViewModel(IField_v2<T> field, ILookUpDataProvider lookUpDataProvider)
            : base(field)
        {
            LookUpData = new List<T>();
            _lookUpDataProvider = lookUpDataProvider;
            _lookUpDataProvider.GetLookupData<T>().Run(LookUpData.Add);
        }

        public IList<T> LookUpData { get; private set; }
    }
}