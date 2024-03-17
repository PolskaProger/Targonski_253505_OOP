using SceletonOfProj_OOP_.DataLayer;
using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var connectionString = "mongodb+srv://AdminUser:011020041@csharpcluster.nbrwntx.mongodb.net/";
        var dbName = "CSharpCluster";
        var dataStorage = new DataStorage(connectionString, dbName);
        var categoryRep = new CategoryRep(dataStorage);
        var positionRep = new PositionRep(dataStorage);
        var orderRep = new OrderRep(dataStorage);
        var userRep = new UserRep(dataStorage);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Работа с категориями");
            Console.WriteLine("2. Работа с позициями");
            Console.WriteLine("3. Работа с заказами");
            Console.WriteLine("4. Работа с пользователями");
            Console.WriteLine("5. Выход");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    // Работа с категориями
                    break;
                case "2":
                    // Работа с позициями
                    break;
                case "3":
                    // Работа с заказами
                    break;
                case "4":
                    // Работа с пользователями
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}
