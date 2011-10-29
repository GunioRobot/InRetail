//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Original file name: ProductCatalogDal.cs
// Generation date: 12/8/2009 12:11:03 PM
namespace ProductCatalogModel
{

    /// <summary>
    /// There are no comments for ProductCatalogContainer in the schema.
    /// </summary>
    public partial class ProductCatalogContainer : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// Initialize a new ProductCatalogContainer object.
        /// </summary>
        public ProductCatalogContainer(global::System.Uri serviceRoot) :
                base(serviceRoot)
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for ProductDetailViewModels in the schema.
        /// </summary>
        public global::System.Data.Services.Client.DataServiceQuery<ProductDetailViewModel> ProductDetailViewModels
        {
            get
            {
                if ((this._ProductDetailViewModels == null))
                {
                    this._ProductDetailViewModels = base.CreateQuery<ProductDetailViewModel>("ProductDetailViewModels");
                }
                return this._ProductDetailViewModels;
            }
        }
        private global::System.Data.Services.Client.DataServiceQuery<ProductDetailViewModel> _ProductDetailViewModels;
        /// <summary>
        /// There are no comments for ProductDetailViewModels in the schema.
        /// </summary>
        public void AddToProductDetailViewModels(ProductDetailViewModel productDetailViewModel)
        {
            base.AddObject("ProductDetailViewModels", productDetailViewModel);
        }
    }
    /// <summary>
    /// There are no comments for ProductCatalogModel.ProductDetailViewModel in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("ProductDetailViewModels")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class ProductDetailViewModel : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new ProductDetailViewModel object.
        /// </summary>
        /// <param name="ID">Initial value of Id.</param>
        /// <param name="description">Initial value of Description.</param>
        public static ProductDetailViewModel CreateProductDetailViewModel(global::System.Guid ID, string description)
        {
            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel();
            productDetailViewModel.Id = ID;
            productDetailViewModel.Description = description;
            return productDetailViewModel;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        public global::System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Description in the schema.
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }
        private string _Description;
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
