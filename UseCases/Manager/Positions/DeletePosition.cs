using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager.Positions
{
    public class DeletePosition
    {
        public static void DeletePositionMenu(Position position, PositionRep positionRep)
        {
            Console.Clear();
            Console.WriteLine("Пункт удаления позиции");
            Console.WriteLine("Выберите категорию из имеющихся: ");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите имя категории, в которой хотите удалить позицию: ");
            var categoryChooseForDeletePosition = Console.ReadLine();
            var categoryFindForDelete = Category.categories.Find(c => c.Name == categoryChooseForDeletePosition);
            if (categoryFindForDelete != null)
            {
                Console.WriteLine($"Вы выбрали категорию {categoryFindForDelete.Name}");
                Console.WriteLine($"Все позиции в данной категории:");
                List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForDelete.Name);
                foreach (Position p in allPositionsInCategory)
                {
                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", id: \"{p.Id}\"");
                }
                Console.WriteLine("Введите имя позиции, которую хотите удалить");
                var positionNameDelete = Console.ReadLine();
                bool resPositionDelete;
                var DelPosition = position.GetPositionByName(positionNameDelete);
                resPositionDelete = position.DeletePosition(positionNameDelete);
                if (resPositionDelete == true)
                {
                    positionRep.Delete(positionNameDelete);
                    Console.WriteLine("Вы удалили позицию!");
                }
                else
                {
                    Console.WriteLine("Ошибка, позиция не была удалена");
                }
            }
            else
            {
                Console.WriteLine("Ошибка, такой категории нет!");
            }
        }
    }
}
