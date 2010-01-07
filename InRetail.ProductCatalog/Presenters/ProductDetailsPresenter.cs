using InRetail.ProductCatalog.PersistenceViewModel;
using InRetail.ProductCatalog.Views;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace InRetail.ProductCatalog.Presenters
{
    public class ProductDetailsPresenter : IScreen<ProductDetailViewModel>
    {
        private readonly IProductDetailView _productDetailView;

        public ProductDetailsPresenter(IProductDetailView productDetailView)
        {
            _productDetailView = productDetailView;
        }

        public void Dispose()
        {
        }

        public object View
        {
            get { return _productDetailView; }
        }

        public string Title
        {
            get { return "Product Details"; }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
        }

        public bool CanClose()
        {
            return true;
        }

        public ProductDetailViewModel Subject
        {
            get { return new ProductDetailViewModel(); }
        }
    }
}