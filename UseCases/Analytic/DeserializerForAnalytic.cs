using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Analytic
{
    public class DeserializerForAnalytic
    {
        public static void DeserializerForAnalyticMenu(Order order, OrderRep orderRep,Serializer serializer)
        {
            string deserializedDataPath = @"D:\University\253505\ООП\OOP(Skeleton)\SerData";
            string ordersJsonPathDes = Path.Combine(deserializedDataPath, "orders.json");
            string ordersCSVPathDes = Path.Combine(deserializedDataPath, "orders.csv");

            Console.WriteLine("Пункт десереализации");
            Console.WriteLine("Выберите из какого формата вы хотите получить данные: ");
            Console.WriteLine("1. CSV");
            Console.WriteLine("2. JSON-файл");
            var chooseForAnalyticDes = Console.ReadLine();
            switch (chooseForAnalyticDes)
            {
                case "1":
                    List<Ord> ordFromCSVOrdClass = serializer.DeserializeFromCsv(ordersCSVPathDes);
                    List<Order> ordersFromCSV = ordFromCSVOrdClass.Select(ord => new Order
                    {
                        Contact = ord.Contact,
                        CustomerName = ord.CustomerName,
                        Id = ord.Id,
                        Date = ord.Date,
                        TotalCost = ord.TotalCost,
                        PositionsInOrder = ord.PositionsInOrder.Select(pos => new Position
                        {
                            Name = pos.Name,
                            Cost = pos.Cost,
                            category = pos.category,
                            Quantity = pos.Quantity,
                            Id = pos.Id
                        }).ToList()
                    }).ToList();
                    foreach (Order newOrder in ordersFromCSV)
                    {
                        orderRep.Add(newOrder);
                        order.AddOrderToList(newOrder);
                    }
                    Console.WriteLine("Десериализация из CSV произведена успешно!");
                    break;
                case "2":
                    List<Order> ordersFromJSON;
                    List<Ord> ordClassJSON = Serializer.DeserializeFromJson(ordersJsonPathDes);
                    ordersFromJSON = ordClassJSON.Select(ord => new Order
                    {
                        Contact = ord.Contact,
                        CustomerName = ord.CustomerName,
                        Id = ord.Id,
                        Date = ord.Date,
                        TotalCost = ord.TotalCost,
                        PositionsInOrder = ord.PositionsInOrder.Select(pos => new Position
                        {
                            Name = pos.Name,
                            Cost = pos.Cost,
                            category = pos.category,
                            Quantity = pos.Quantity,
                            Id = pos.Id
                        }).ToList()
                    }).ToList();
                    foreach (Order newOrder in ordersFromJSON)
                    {
                        orderRep.Add(newOrder);
                        order.AddOrderToList(newOrder);
                    }
                    Console.WriteLine("Десериализация из JSON произведена успешно!");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}
