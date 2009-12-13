using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace InRetail.ProductCatalog.Web
{
    public class ProductCatalog : DataService<ProductCatalogContainer>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.DataServiceBehavior.MaxProtocolVersion=DataServiceProtocolVersion.V2;
        }
    }
}
