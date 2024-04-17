using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Casher
{
    public class DeleteOrder
    {
        public static void DeleteOrderMenu(OrderRep orderRep) 
        {
            Console.WriteLine("Пункт удаления заказа");
            Console.WriteLine("Список всех заказов:");
            foreach (Order o in Order.orders)
            {
                Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                List<Position> posInOrder = o.PositionsInOrder;
                foreach (Position p in posInOrder)
                {
                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", колличество позиции {p.Quantity}, id: \"{p.Id}\"");
                }
            }
            Console.WriteLine("Введите контактные данные заказчика, заказ которого хотите удалить:");
            var contactTempDel = Console.ReadLine();
            var orderToDel = Order.orders.Find(o => o.Contact == contactTempDel);
            if (orderToDel != null)
            {
                bool resOrderToDel = orderToDel.DeleteOrder(contactTempDel);
                if (resOrderToDel)
                {
                    orderRep.Delete(orderToDel.Contact);
                    Console.WriteLine($"Заказ на контактные данные {contactTempDel} удалён!");
                }
                else
                {
                    Console.WriteLine("Ошибка, заказа с данным именем не существует!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка, заказа с данным именем не существует!");
            }
        }
    }
}
