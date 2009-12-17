using System;

namespace InRetail.ProductCatalog.QueryModel
{
    public class ProductDetailViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
    }
}