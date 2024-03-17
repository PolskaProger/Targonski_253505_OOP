using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class Category
    {
        public string Name { get; set; }
        public string Id { get; set; }

        private static List<Category> categories = new List<Category>();
        public Category CreateCategory(string name)
        {
            return new Category { Name = name, Id = Guid.NewGuid().ToString() };
        }

        public void EditCategory(string id, string newName)
        {
            // Assuming 'categories' is a list of Category objects
            var category = categories.Find(c => c.Id == id);
            if (category != null)
            {
                category.Name = newName;
            }
        }

        public void DeleteCategory(string id)
        {
            // Assuming 'categories' is a list of Category objects
            categories.RemoveAll(c => c.Id == id);
        }
    }
}
