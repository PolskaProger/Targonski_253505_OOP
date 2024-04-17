using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager.Categories
{
    public class DeleteCategory
    {
        public static void DeletedCategoryMenu(Category category, CategoryRep categoryRep)
        {
            Console.Clear();
            Console.WriteLine("Пункт удаления категории");
            Console.WriteLine("Список существующих позиций:");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"\tКатегория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите название категории, которую хотите удалить:");
            var categoryNameTempDelete = "";
            categoryNameTempDelete = Console.ReadLine();
            var DelCategory = category.GetCategoryByName(categoryNameTempDelete);
            if (DelCategory != null)
            {
                DelCategory.DeleteCategory(categoryNameTempDelete);
                categoryRep.Delete(categoryNameTempDelete);
                Console.WriteLine($"Категория с именем \"{categoryNameTempDelete}\" была удалена из БД!");
            }
            else
            {
                Console.WriteLine($"Ошибка, категории с именем \"{categoryNameTempDelete}\" не существует!");
            }
        }
    }
}
