using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.ForAllUsers
{
    public class HistoryOfOrders
    {
        public static void HistoryOfOrdersMenu() 
        {
            Console.Clear();
            Console.WriteLine("Пункт истории заказов");
            Console.WriteLine("Список всех заказов:");
            foreach (Order o in Order.orders)
            {
                Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                List<Position> posInOrder = o.PositionsInOrder;
                foreach (Position p in posInOrder)
                {
                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", колличество позиции {p.Quantity}, id: \"{p.Id}\"");
                }
            }
        }
    }
}
