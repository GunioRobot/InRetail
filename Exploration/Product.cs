using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Exploration
{
    public class Category : IComparable<Category>, IComparable
    {
        public Category()
        {
            ChildCategories = new ObservableCollection<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        private Category _parentCategory;
        public Category ParentCategory
        {
            get { return _parentCategory; }
            set
            {
                if (_parentCategory != null)
                    _parentCategory.RemoveChildCategory(this);
                value.AddChildCategory(this);
                _parentCategory = value;
            }
        }

        private void RemoveChildCategory(Category category)
        {
            ChildCategories.Remove(category);
        }

        private void AddChildCategory(Category category)
        {
            ChildCategories.Add(category);
        }

        public IList<Category> ChildCategories
        {
            get;
            private set;
        }

        public int CompareTo(Category other)
        {
            return Id.CompareTo(other.Id);
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo((Category)obj);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}