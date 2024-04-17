using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager.Categories
{
    public class UpdateCategory
    {
        public static void UpdateCategoryMenu(Category category, CategoryRep categoryRep)
        {

            Console.Clear();
            Console.WriteLine("Пункт изменения категории");
            Console.WriteLine("Список существующих позиций:");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"\tКатегория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите название старой категории:");
            var categoryNameTempOld = Console.ReadLine();
            Console.WriteLine("Введите новое название категории:");
            var categoryNameTempNew = Console.ReadLine();
            var oldCategory = category.GetCategoryByName(categoryNameTempOld);
            if (oldCategory != null)
            {
                oldCategory.EditCategory(categoryNameTempOld, categoryNameTempNew);
                categoryRep.Update(oldCategory);
                Console.WriteLine($"Категория с именем \"{categoryNameTempOld}\" изменена на имя \"{categoryNameTempNew}\" и добавлена в БД!");
            }
            else
            {
                Console.WriteLine($"Ошибка, категории с именем \"{categoryNameTempOld}\" не существует!");
            }
        }
    }
}
