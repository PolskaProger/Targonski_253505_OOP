using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager
{
    public class FullAsortiment
    {
        public static void FullAsortimentMenu()
        {
            Console.Clear();
            Console.WriteLine("Пункт ассортимента доступной продукции:");
            Console.WriteLine("Выберите категорию из имеющихся: ");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите имя категории, в которой хотите посмотреть позиции: ");
            var categoryChooseForAllPositions = Console.ReadLine();
            var categoryFindForAllPositions = Category.categories.Find(c => c.Name == categoryChooseForAllPositions);
            if (categoryFindForAllPositions != null)
            {
                Console.WriteLine($"Вы выбрали категорию {categoryFindForAllPositions.Name}");
                Console.WriteLine($"Все позиции в данной категории:");
                List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForAllPositions.Name);
                foreach (Position p in allPositionsInCategory)
                {
                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", id: \"{p.Id}\"");
                }
            }
            else
            {
                Console.WriteLine("Ошибка, такой категории нет!");
            }
        }
    }
}
