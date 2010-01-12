using System.Collections.Generic;
using System.Linq;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class MessageRefFieldViewModel : MessageValueFieldViewModelBase
    {
        private readonly ILookUpDataProvider _lookUpDataProvider;

        public MessageRefFieldViewModel(IField_v2 field, ILookUpDataProvider lookUpDataProvider)
            : base(field)
        {
            _lookUpDataProvider = lookUpDataProvider;
            
            LookUpData = new List<object>();
            _lookUpDataProvider.GetSuppliers().Run(LookUpData.Add);
        }

        public IList<object> LookUpData { get; set; }
    }
}