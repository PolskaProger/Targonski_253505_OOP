using DocumentFormat.OpenXml.Spreadsheet;
using MongoDB.Driver;
using OfficeOpenXml;
using SceletonOfProj_OOP_.DataLayer;
using SceletonOfProj_OOP_.LogicLayer;
using SceletonOfProj_OOP_.Services;
using SceletonOfProj_OOP_.UseCases.Analytic;
using SceletonOfProj_OOP_.UseCases.Casher;
using SceletonOfProj_OOP_.UseCases.ForAllUsers;
using SceletonOfProj_OOP_.UseCases.Manager;
using SceletonOfProj_OOP_.UseCases.Manager.Categories;
using SceletonOfProj_OOP_.UseCases.Manager.Positions;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        var connectionString = "mongodb://localhost:27017";
        var dbName = "CRM-Database";
        var dataStorage = new DataStorage(connectionString, dbName);
        var categoryRep = new CategoryRep(dataStorage);
        var userRep = new UserRep(dataStorage);
        var positionRep = new PositionRep(dataStorage);
        var orderRep = new OrderRep(dataStorage);

        bool exit = false;

        User user = new User();
        Category category = new Category();
        Position position = new Position();
        Order order = new Order();


        IEnumerable<Category> AllCategories = categoryRep.GetAll();
        Category.categories.AddRange(AllCategories);
        IEnumerable<User> AllUsers = userRep.GetAll();
        User.users.AddRange(AllUsers);
        IEnumerable<Position> AllPositions = positionRep.GetAll();
        Position.positions.AddRange(AllPositions);
        IEnumerable<Order> AllOrders = orderRep.GetAll();
        Order.orders.AddRange(AllOrders);

        Serializer serializer = new Serializer();

        CorrectInputService correct_imput = new CorrectInputService();

        while (!exit)
        {
            bool exitMenuReg = false;
            bool exitMenuAuth = false;
            bool exitAddPosInOrder = false;
            bool exitChangePosInOrder = false;

            Console.WriteLine("Меню входа/регистрации в SIMPLE-CRM!:");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Регистрация пользователя");
            Console.WriteLine("2. Вход в приложение");
            Console.WriteLine("3. Список всех пользлователей");
            Console.WriteLine("4. Выход");

            var choice = Console.ReadLine();
            switch (choice)
            {

                case "1":
                    Registration.RegistrationMenu(correct_imput,exitMenuReg, user, userRep);
                    break;
                case "2":
                    Console.WriteLine("Меню авторизации пользователя:");
                    Console.WriteLine("Введите имя пользователя:");
                    string userNameTempAuth = Console.ReadLine();
                    Console.WriteLine("Введите пароль: ");
                    string passwordTempAuth = Console.ReadLine();
                    var correct_password = correct_imput.ValidatePassword(passwordTempAuth);
                    bool enter= false;
                    if (correct_password == false)
                    {
                        Console.WriteLine("Ошибка, пароль не является валидным! (больше 8 символов, 1 большая и одна прописная латинская буква)");
                    }
                    else
                    {
                        enter = user.Authorize(userNameTempAuth, passwordTempAuth);
                        if (enter == false)
                        {
                            Console.WriteLine("Ошибка авторизации! Неверный логин/пароль или данного пользователя не существует");
                        }
                        else
                        {
                            while (!exitMenuAuth)
                            {
                                Console.WriteLine($"Добро пожаловать в Simple-CRM, {userNameTempAuth}!");
                                if (user.RoleOfUser(userNameTempAuth) == "Кассир")
                                {
                                    Console.WriteLine($"Ваша роль - Кассир");
                                    Console.WriteLine("Выберите действие:");
                                    Console.WriteLine("1.Добавить заказ");
                                    Console.WriteLine("2.Изменить заказ");
                                    Console.WriteLine("3.Удалить заказ");
                                    Console.WriteLine("4.История заказов");
                                    Console.WriteLine("5.Выход из приложения");
                                    var chooseMenuAuth = Console.ReadLine();
                                    switch (chooseMenuAuth)
                                    {
                                        case "1":
                                            AddOrder.AddOrderMenu(correct_imput,exitAddPosInOrder, order, orderRep);
                                            break;
                                        case "2":
                                            UpdateOrder.UpdateOrderMenu(correct_imput,exitChangePosInOrder, exitAddPosInOrder, orderRep);
                                            break;
                                        case "3":
                                            DeleteOrder.DeleteOrderMenu(orderRep);
                                            break;
                                        case "4":
                                            HistoryOfOrders.HistoryOfOrdersMenu();
                                            break;
                                        case "5":
                                            exitMenuAuth = true;
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                            break;
                                    }
                                }
                                if (user.RoleOfUser(userNameTempAuth) == "Бухгалтер")
                                {
                                    Console.WriteLine($"Ваша роль - Бухгалтер");
                                    Console.WriteLine("Выберите действие:");
                                    Console.WriteLine("1.Аналитика за месяц");
                                    Console.WriteLine("2.История заказов");
                                    Console.WriteLine("3.Сериализация заказов");
                                    Console.WriteLine("4.Десерализация заказов");
                                    Console.WriteLine("5.Выход из приложения");
                                    var chooseMenuAuth = Console.ReadLine();
                                    switch (chooseMenuAuth)
                                    {
                                        case "1":
                                            Analytic.AnalyticMenu();
                                            break;
                                        case "2":
                                            HistoryOfOrders.HistoryOfOrdersMenu();
                                            break;
                                        case "3":
                                            SerializerForAnalytic.SerializerForAnalyticMenu(serializer);
                                            break;
                                        case "4":
                                            DeserializerForAnalytic.DeserializerForAnalyticMenu(order, orderRep, serializer);
                                            break;
                                        case "5":
                                            exitMenuAuth = true;
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                            break;
                                    }
                                }
                                if (user.RoleOfUser(userNameTempAuth) == "Менеджер")
                                {
                                    Console.WriteLine($"Ваша роль - Менеджер ");
                                    Console.WriteLine("Выберите действие:");
                                    Console.WriteLine("1.Добавить новую категорию");
                                    Console.WriteLine("2.Изменить категорию");
                                    Console.WriteLine("3.Удалить категорию");
                                    Console.WriteLine("4.Добавить новую позицию");
                                    Console.WriteLine("5.Изменить позицию");
                                    Console.WriteLine("6.Удалить позицию");
                                    Console.WriteLine("7.История заказов");
                                    Console.WriteLine("8.Ассортимент");
                                    Console.WriteLine("9.Выход из приложения");
                                    var chooseMenuAuth = Console.ReadLine();
                                    switch (chooseMenuAuth)
                                    {
                                        case "1":
                                            AddCategory.AddCategoryMenu(category, categoryRep);
                                            break;
                                        case "2":
                                            UpdateCategory.UpdateCategoryMenu(category, categoryRep);
                                            break;
                                        case "3":
                                            DeleteCategory.DeletedCategoryMenu(category, categoryRep);
                                            break;
                                        case "4":
                                            AddPosition.AddPositionMenu(position, positionRep);
                                            break;
                                        case "5":
                                            UpdatePosition.UpdatePositionMenu(position, positionRep);
                                            break;
                                        case "6":
                                            DeletePosition.DeletePositionMenu(position, positionRep);
                                            break;
                                        case "7":
                                            HistoryOfOrders.HistoryOfOrdersMenu();
                                            break;
                                        case "8":
                                            FullAsortiment.FullAsortimentMenu();
                                            break;
                                        case "9":
                                            exitMenuAuth = true;
                                            break;
                                        default:
                                            Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("Список пользователей:");
                    foreach (User u in User.users)
                    {
                        Console.WriteLine($"Имя пользователя: {u.Login}, роль: {u.Role}");
                    }
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}
