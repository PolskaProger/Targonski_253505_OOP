using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager.Categories
{
    public class AddCategory
    {
        public static void AddCategoryMenu(Category category, CategoryRep categoryRep)
        {
            Console.Clear();
            Console.WriteLine("Пункт добавления категории");
            Console.WriteLine("Список существующих позиций:");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"\tКатегория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите название новой категории:");
            var categoryNameTemp = Console.ReadLine();
            Category findCategory = Category.categories.Find(c => c.Name == categoryNameTemp);
            if (findCategory != null)
            {
                Console.WriteLine("Ошибка. Категория с данным именем уже существует!");
            }
            else
            {
                Category newCategory = category.CreateCategory(categoryNameTemp);
                Console.WriteLine($"Категория с именем \"{newCategory.Name}\" создана!");
                categoryRep.Add(newCategory);
                Console.WriteLine($"Категория с именем \"{newCategory.Name}\" добавлена в БД.");
            }
        }
    }
}
