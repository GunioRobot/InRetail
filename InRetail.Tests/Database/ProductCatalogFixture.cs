using System;
using System.Data;
using System.Data.SqlClient;

namespace Tests.InRetail.Database
{
    public class ProductCatalogFixture
    {
        public void AddProducts()
        {
            var command = new SqlCommand("Insert Into ProductDetailViewModels values(@id,@name)");
            command.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@name", SqlDbType.NVarChar);
 
            using(var con =new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProductCatalog;Integrated Security=True"))
            {
                con.Open();
                command.Connection = con;
                for (int i = 0; i < 40000; i++)
                {
                
                
                command.Parameters["@id"].Value = Guid.NewGuid();
                command.Parameters["@name"].Value = "asjk askdjn asj dnasdm,asndm,an dm,a ";
                command.ExecuteNonQuery();
}
                con.Close();
            }
        }
    }
}