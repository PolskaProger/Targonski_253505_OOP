using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Manager.Positions
{
    public class AddPosition
    {
        public static void AddPositionMenu(Position position, PositionRep positionRep)
        {
            Console.Clear();
            Console.WriteLine("Пункт добавления позиции");
            Console.WriteLine("Выберите категорию из имеющихся: ");
            foreach (Category c in Category.categories)
            {
                Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
            }
            Console.WriteLine("Введите имя категории, в которую хотите добавить позицию: ");
            var categoryChoose = Console.ReadLine();
            var categoryFind = Category.categories.Find(c => c.Name == categoryChoose);
            if (categoryFind != null)
            {
                Console.WriteLine($"Вы выбрали категорию {categoryFind.Name}");
                Console.WriteLine($"Все позиции в данной категории:");
                List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFind.Name);
                foreach (Position p in allPositionsInCategory)
                {
                    Console.WriteLine($" Позиция: {p.Name}, стоимость: {p.Cost}$, категория позиции: {p.category}, id: {p.Id}");
                }
                Console.WriteLine("Введите имя новой позиции");
                var positionName = Console.ReadLine();
                Console.WriteLine("Введите стоимость позиции (валюта - доллары США)");
                float positionCost = 0;
                positionCost = float.Parse(Console.ReadLine());
                Position findPosition = Position.positions.Find(p=>p.Name==positionName);
                if (findPosition != null)
                {
                    Console.WriteLine("Ошибка! Данная позиция уже существует!");
                }
                else
                {
                    var newPosition = position.CreatePosition(positionName, positionCost, categoryFind.Name);
                    positionRep.Add(newPosition);
                    Console.WriteLine("Вы создали новую позицию и она была добавлена в БД!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка, такой категории нет!");
            }
        }
    }
}
