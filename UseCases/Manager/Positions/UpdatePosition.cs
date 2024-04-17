using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager.Positions
{
    public class UpdatePosition
    {
        public static void UpdatePositionMenu(Position position, PositionRep positionRep)
        {
            Console.Clear();
            Console.WriteLine("Пункт обновления позиции");
            Console.WriteLine("Выберите категорию из имеющихся: ");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите имя категории, в которой хотите изменить позицию: ");
            var categoryChooseForUpdate = Console.ReadLine();
            var categoryFindForUpdate = Category.categories.Find(c => c.Name == categoryChooseForUpdate);
            if (categoryFindForUpdate != null)
            {
                Console.WriteLine($"Вы выбрали категорию {categoryFindForUpdate.Name}");
                Console.WriteLine($"Все позиции в данной категории:");
                List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForUpdate.Name);
                foreach (Position p in allPositionsInCategory)
                {
                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", id: \"{p.Id}\"");
                }
                Console.WriteLine("Введите имя позиции, которую хотите изменить");
                var positionNameOld = Console.ReadLine();
                Console.WriteLine("Введите новое имя позиции: ");
                var positionNameNew = Console.ReadLine();
                Console.WriteLine("Введите новую стоимость позиции (валюта - доллары США)");
                float positionCostNew = float.Parse(Console.ReadLine());
                var oldPosition = position.GetPositionByName(positionNameOld);
                if (oldPosition != null)
                {
                    oldPosition.EditPosition(positionNameOld, positionNameNew, positionCostNew);
                    positionRep.Update(oldPosition);
                    Console.WriteLine("Вы обновили позицию!");
                }
                else
                {
                    Console.WriteLine("Ошибка, позиция не была обновлена");
                }
            }
            else
            {
                Console.WriteLine("Ошибка, такой категории нет!");
            }
        }
    }
}
