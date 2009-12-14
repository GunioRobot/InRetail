using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Exploration
{
    public class Products : ObservableCollection<Product>
    {
        public Products()
        {
            var categories = new List<Category>();

            var rootCat = new Category() {Id = 1, Name = "All Products"};
            var channelCat = new Category() {Id = 2, Name = "Channel", ParentCategory = rootCat};
            var bossCat = new Category() { Id = 3, Name = "Boss", ParentCategory = rootCat };
            var bossManCat = new Category() { Id = 4, Name = "Boss Man", ParentCategory = bossCat };
            var bossWomanCat = new Category() { Id = 5, Name = "Boss Woman", ParentCategory = bossCat };
            categories.Add(rootCat);
            categories.Add(channelCat);
            categories.Add(bossCat);
            categories.Add(bossManCat);
            categories.Add(bossWomanCat);


            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 5000; i++)
            {
                Add(new Product {Id = i, Category  = categories[random.Next(0,5)],Description = "Product " + i, Price= random.NextDouble()*100});
            }
        }

    }

}