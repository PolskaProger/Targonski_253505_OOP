using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Analytic
{
    public class SerializerForAnalytic
    {
        public static void SerializerForAnalyticMenu(Serializer serializer)
        {
            string serializedDataPath = @"D:\University\253505\ООП\OOP(Skeleton)\SerData";
            string ordersJsonPath = Path.Combine(serializedDataPath, "orders.json");
            string ordersCSVPath = Path.Combine(serializedDataPath, "orders.csv");
            Console.WriteLine("Пункт сереализации");
            Console.WriteLine("Выберите в каком формате вы хотите получить данные: ");
            Console.WriteLine("1. CVS");
            Console.WriteLine("2. JSON-файл");
            var chooseForAnalytic = Console.ReadLine();
            switch (chooseForAnalytic)
            {
                case "1":
                    List<Ord> ordersToSerialize = Order.orders.Select(o => new Ord
                    {
                        CustomerName = o.CustomerName,
                        Contact = o.Contact,
                        PositionsInOrder = o.PositionsInOrder.Select(p => new Pos
                        {
                            Name = p.Name,
                            Cost = p.Cost,
                            category = p.category,
                            Quantity = p.Quantity,
                            Id = p.Id
                        }).ToList(),
                        TotalCost = o.TotalCost,
                        Date = o.Date,
                        Id = o.Id
                    }).ToList();
                    Ord.orders.AddRange(ordersToSerialize);
                    serializer.SerializeToCsv(Ord.orders, ordersCSVPath);
                    Console.WriteLine("Сериализация в CSV произведена успешно!");
                    break;
                case "2":
                    serializer.SerializeToJson(Order.orders, ordersJsonPath);
                    Console.WriteLine("Сериализация в JSON произведена успешно!");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}
