using System.Collections.ObjectModel;
using System;

namespace Exploration
{
    public class Products : ObservableCollection<Product>
    {
        public Products()
        {
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 5; i++)
            {
                Add(new Product {Id = i, Description = "Product " + i,Price=random.NextDouble()*100});
            }
        }
    }
}