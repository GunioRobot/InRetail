using System;
using System.Data.EntityClient;
using System.Linq;


namespace InRetail.ProductCatalog.Web
{
    public class TestFixture
    {
        public void Test()
        {
            var connection = new EntityConnection(@"metadata=res://*/ProductCatalog.csdl|res://*/ProductCatalog.ssdl|res://*/ProductCatalog.msl;provider=System.Data.SqlClient;provider connection string='Data Source=sr01.lutecia.ge\SQLEXPRESS,1433;Initial Catalog=ProductCatalog;User ID=sa;Password=tatu;MultipleActiveResultSets=True';");
            using (var context = new ProductCatalogContainer(connection))
            {
                Console.WriteLine(context.ProductDetailViewModels.FirstOrDefault());
            }
        }
    }
}