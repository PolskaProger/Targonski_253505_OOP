using MongoDB.Driver;
using SceletonOfProj_OOP_.DataLayer;
using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

class Program
{

    static void Main(string[] args)
    {
       // var connectionString = "mongodb+srv://AdminUser:011020041@csharpcluster.nbrwntx.mongodb.net/";
        //var dbName = "CSharpCluster";
        //var dataStorage = new DataStorage(connectionString, dbName);

        bool exit = false;

        User user = new User();
        Category category = new Category();
        Position position = new Position();
        Order order = new Order();

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
                    while (!exitMenuReg)
                    {
                        Console.WriteLine("Меню регистрации пользователя:");
                        Console.WriteLine("Введите имя пользователя:");
                        string userNameTemp = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        string passwordTemp = Console.ReadLine();
                        Console.WriteLine("Выберите роль пользователя: ");
                        Console.WriteLine("1.Кассир");
                        Console.WriteLine("2.Менеджер");
                        Console.WriteLine("3.Бухгалтер");
                        var roleTempChoice = Console.ReadLine();
                        string roleTemp = "";
                        switch (roleTempChoice)
                        {
                            case "1":
                                roleTemp = "Кассир";
                                break;
                            case "2":
                                roleTemp = "Менеджер";
                                break;
                            case "3":
                                roleTemp = "Бухгалтер";
                                break;
                            default:
                                Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                break;
                        }
                        user.Register(userNameTemp, passwordTemp, roleTemp);
                        Console.WriteLine($"Пользователь под именем {userNameTemp} успешно создан!");
                        Console.WriteLine("Хотите создать нового пользователя?");
                        Console.WriteLine("1.Да");
                        Console.WriteLine("2.Нет");
                        var choiceMenuReg = Console.ReadLine();
                        switch(choiceMenuReg)
                        {
                            case "1":
                                break; 
                            case "2":
                                Console.WriteLine("Выход из меню создания пользователя");
                                exitMenuReg = true;
                                break;
                            default:
                                Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                break;
                        }
                    }
                    break;
                case "2":
                        Console.WriteLine("Меню авторизации пользователя:");
                        Console.WriteLine("Введите имя пользователя:");
                        string userNameTempAuth = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        string passwordTempAuth = Console.ReadLine();
                        bool enter= false;
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
                                        exitAddPosInOrder = false;
                                        Console.WriteLine("Пункт добаления заказа");
                                        Console.WriteLine("Введите имя заказчика:");
                                        var customerNameTemp = "";
                                        customerNameTemp = Console.ReadLine();
                                        Console.WriteLine("Введите контактную информацию (email):");
                                        var contactInfoTemp = "";
                                        contactInfoTemp = Console.ReadLine();
                                        Console.WriteLine("Введите дату заказа в формате (dd.MM.yyyy):");
                                        DateTime dateOfOrder;
                                        string dateStr = Console.ReadLine();
                                        if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfOrder))
                                        {
                                            Console.WriteLine("Введенная дата: " + dateOfOrder.ToString("dd.MM.yyyy"));
                                        }
                                        else
                                        {
                                            Console.WriteLine("Некорректный формат даты.");
                                        }
                                        List<Position> positionsInOrder= [];
                                       
                                        ////////////////////////////////////////////////////////
                                        while(!exitAddPosInOrder)
                                        {
                                            Console.WriteLine("Добавление позиций в заказ");
                                            Console.WriteLine("Выберите категорию из имеющихся: ");
                                            List<Category> assOfCategory = Category.GetAllCategories();
                                            foreach (Category c in assOfCategory)
                                            {
                                                Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                                            }
                                            Console.WriteLine("Введите имя категории, из которой хотите добавить позицию в заказ: ");
                                            var categoryChooseForAllPositions = Console.ReadLine();
                                            var categoryFindForAllPositions = assOfCategory.Find(c => c.Name == categoryChooseForAllPositions);
                                            if (categoryFindForAllPositions != null)
                                            {
                                                Console.WriteLine($"Вы выбрали категорию {categoryFindForAllPositions.Name}");
                                                Console.WriteLine($"Все позиции в данной категории:");
                                                List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForAllPositions.Name);
                                                foreach (Position p in allPositionsInCategory)
                                                {
                                                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                                }
                                                Console.WriteLine("Выберите имя позиция, которую хотите добавить в заказ:");
                                                var namePositionForOrder = Console.ReadLine();
                                                var choosePositionForOrder = allPositionsInCategory.Find(p => p.Name == namePositionForOrder);
                                                if (choosePositionForOrder != null)
                                                {
                                                    Console.WriteLine("Позиция добавлена в заказ");
                                                    positionsInOrder.Add(choosePositionForOrder);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ошибка, такой позиции нет!");
                                                }

                                                Console.WriteLine("Хотите добавить ещё позиций в заказ?");
                                                Console.WriteLine("1.Да");
                                                Console.WriteLine("2.Нет");
                                                var choiceAddPosInOrder = Console.ReadLine();
                                                switch (choiceAddPosInOrder)
                                                {
                                                    case "1":
                                                        break;
                                                    case "2":
                                                        Console.WriteLine("Выход из меню добавления позиций.");
                                                        exitAddPosInOrder = true;
                                                        break;
                                                    default:
                                                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ошибка, такой категории нет!");
                                            }
                                        }
                                        Order resOrder = order.CreateOrder(customerNameTemp, contactInfoTemp, dateOfOrder, positionsInOrder);
                                        Console.WriteLine($"Заказ на имя {customerNameTemp} создан! на общую сумму {resOrder.TotalCost}$");
                                        break;
                                    case "2":
                                        Console.WriteLine("Пункт изменения заказа");
                                        List<Order> orderList = Order.GetAllOrders();
                                        foreach (Order o in orderList)
                                        {
                                            Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                                            Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                                            List<Position> posInOrderToUpdate = o.PositionsInOrder;
                                            foreach (Position p in posInOrderToUpdate)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                        }
                                        Console.WriteLine("Введите контактные данные заказчика, заказ которого хотите изменить:");
                                        var contactTemp = Console.ReadLine();
                                        exitChangePosInOrder = false;
                                        var orderToUpdate = orderList.Find(o=>o.Contact==contactTemp);
                                        if (orderToUpdate != null)
                                        {
                                            Console.WriteLine("Введите новое имя заказчика:");
                                            var newCustomerName = Console.ReadLine();
                                            Console.WriteLine("Введите новые контактные данные");
                                            var newContactName = Console.ReadLine();
                                            while (!exitChangePosInOrder)
                                            {
                                                Console.WriteLine("Меню для изменения позиций в заказе:");
                                                Console.WriteLine("Все позиции в заказе на имя ");
                                                List<Position> posInOrderToUpdateTemp = orderToUpdate.PositionsInOrder;
                                                foreach (Position p in posInOrderToUpdateTemp)
                                                {
                                                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                                }
                                                Console.WriteLine("1.Добавить новую позицию в заказ");
                                                Console.WriteLine("2.Удалить старую позицию в заказе");
                                                var choiceMenuPosInOrder = Console.ReadLine();
                                                switch (choiceMenuPosInOrder)
                                                {
                                                    case "1":
                                                        while (!exitAddPosInOrder)
                                                        {
                                                            Console.WriteLine("Добавление позиций в заказ");
                                                            Console.WriteLine("Выберите категорию из имеющихся: ");
                                                            List<Category> assOfCategory = Category.GetAllCategories();
                                                            foreach (Category c in assOfCategory)
                                                            {
                                                                Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                                                            }
                                                            Console.WriteLine("Введите имя категории, из которой хотите добавить позицию в заказ: ");
                                                            var categoryChooseForAllPositions = Console.ReadLine();
                                                            var categoryFindForAllPositions = assOfCategory.Find(c => c.Name == categoryChooseForAllPositions);
                                                            if (categoryFindForAllPositions != null)
                                                            {
                                                                Console.WriteLine($"Вы выбрали категорию {categoryFindForAllPositions.Name}");
                                                                Console.WriteLine($"Все позиции в данной категории:");
                                                                List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForAllPositions.Name);
                                                                foreach (Position p in allPositionsInCategory)
                                                                {
                                                                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                                                }
                                                                Console.WriteLine("Выберите имя позиция, которую хотите добавить в заказ:");
                                                                var namePositionForOrder = Console.ReadLine();
                                                                var choosePositionForOrder = allPositionsInCategory.Find(p => p.Name == namePositionForOrder);
                                                                if (choosePositionForOrder != null)
                                                                {
                                                                    Console.WriteLine("Позиция добавлена в заказ");
                                                                    posInOrderToUpdateTemp.Add(choosePositionForOrder);
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Ошибка, такой позиции нет!");
                                                                }

                                                                Console.WriteLine("Хотите добавить ещё позиций в заказ?");
                                                                Console.WriteLine("1.Да");
                                                                Console.WriteLine("2.Нет");
                                                                var choiceAddPosInOrder = Console.ReadLine();
                                                                switch (choiceAddPosInOrder)
                                                                {
                                                                    case "1":
                                                                        break;
                                                                    case "2":
                                                                        Console.WriteLine("Выход из меню добавления позиций.");
                                                                        exitAddPosInOrder = true;
                                                                        break;
                                                                    default:
                                                                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                                                        break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Ошибка, такой категории нет!");
                                                            }
                                                        }
                                                        break;
                                                    case "2":
                                                        Console.WriteLine("Пункт удаления позиции из заказа");
                                                        Console.WriteLine($"Все позиции в заказе на имя {orderToUpdate.CustomerName}");
                                                        var allPosInOrderToDel = orderToUpdate.GetAllPosOfOrder(contactTemp);
                                                        foreach (Position p in allPosInOrderToDel)
                                                        {
                                                            Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                                        }
                                                        Console.WriteLine("Введите имя позиции которую хотите удалить из заказа");
                                                        var nameOfPosToDelFromOrder = Console.ReadLine();
                                                        var posDelFromOrder = allPosInOrderToDel.Find(p => p.Name == nameOfPosToDelFromOrder);
                                                        if (posDelFromOrder!=null)
                                                        {
                                                            allPosInOrderToDel.RemoveAll(p=>p.Name==nameOfPosToDelFromOrder);
                                                            Console.WriteLine("Позиция успешно удалена из заказа!");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Ошибка, данной позиции не существует в заказе!");
                                                        }
                                                        break;
                                                    default:
                                                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                                        break;
                                                }
                                                Console.WriteLine("Хотите ещё изменить позиции в заказе?");
                                                Console.WriteLine("1.Да");
                                                Console.WriteLine("2.Нет");
                                                var choiceChangePosInOrder = Console.ReadLine();
                                                switch (choiceChangePosInOrder)
                                                {
                                                    case "1":
                                                        break;
                                                    case "2":
                                                        Console.WriteLine("Выход из меню изменения позиций.");
                                                        exitChangePosInOrder = true;
                                                        break;
                                                    default:
                                                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                                                        break;
                                                }
                                            }
                                            orderToUpdate.EditOrder(contactTemp, newCustomerName, newContactName, orderToUpdate.PositionsInOrder);
                                            Console.WriteLine("Заказ успешно обновлён!");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Ошибка, заказа на контактные данные {contactTemp} не существует!");
                                        }
                                        break;
                                    case "3":
                                        Console.WriteLine("Пункт удаления заказа");
                                        Console.WriteLine("Список всех заказов:");
                                        List<Order> orderListDel = Order.GetAllOrders();
                                        foreach (Order o in orderListDel)
                                        {
                                            Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                                            Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                                            List<Position> posInOrder = o.PositionsInOrder;
                                            foreach (Position p in posInOrder)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                        }
                                        Console.WriteLine("Введите контактные данные заказчика, заказ которого хотите удалить:");
                                        var contactTempDel = Console.ReadLine();
                                        var orderToDel = orderListDel.Find(o=>o.Contact == contactTempDel);
                                        if(orderToDel!= null)
                                        {
                                            bool resOrderToDel = orderToDel.DeleteOrder(contactTempDel);
                                            if (resOrderToDel)
                                            {
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
                                        break;
                                    case "4":
                                        Console.WriteLine("Пункт истории заказов");
                                        Console.WriteLine("Список всех заказов:");
                                        List<Order> allOrders = Order.GetAllOrders();
                                        foreach (Order o in allOrders)
                                        {
                                            Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                                            Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                                            List<Position> posInOrder = o.PositionsInOrder;
                                            foreach (Position p in posInOrder)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                        }
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
                                Console.WriteLine("3.Выход из приложения");
                                var chooseMenuAuth = Console.ReadLine();
                                switch (chooseMenuAuth)
                                {
                                    case "1":
                                        Console.WriteLine("Пункт аналитики:");
                                        var analyticsService = new AnalyticsService();

                                        var averageCheck = analyticsService.CalculateAverageCheck();
                                        Console.WriteLine($"Средний чек по всем заказам: {averageCheck}");

                                        var month = DateTime.Now.Month;
                                        var year = DateTime.Now.Year;
                                        var monthlyAverageCheck = analyticsService.CalculateMonthlyAverageCheck(year, month);
                                        Console.WriteLine($"Средний чек за текущий месяц: {monthlyAverageCheck}");

                                        var (CustomerName, Contact) = analyticsService.GetMostValuableCustomer();
                                        Console.WriteLine($"Самый платежеспособный клиент: {CustomerName}, Контакт: {Contact}");

                                        var mostPopularPosition = analyticsService.GetMostPopularPosition();
                                        Console.WriteLine($"Самая популярная позиция: {mostPopularPosition?.Name ?? "Нет данных"}");

                                        break;

                                    case "2":
                                        Console.WriteLine("Пункт истории заказов");
                                        Console.WriteLine("Список всех заказов:");
                                        List<Order> allOrders = Order.GetAllOrders();
                                        foreach (Order o in allOrders)
                                        {
                                            Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                                            Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                                            List<Position> posInOrder = o.PositionsInOrder;
                                            foreach (Position p in posInOrder)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                        }

                                        break;
                                    case "3":
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
                                        Console.WriteLine("Пункт добавления категории");
                                        Console.WriteLine("Введите название категории:");
                                        var categoryNameTemp = "";
                                        categoryNameTemp = Console.ReadLine();
                                        category.CreateCategory(categoryNameTemp);
                                        Console.WriteLine($"Категория с именем \"{categoryNameTemp}\" создана!");
                                        break;
                                    case "2":
                                        Console.WriteLine("Пункт изменения категории");
                                        Console.WriteLine("Введите название старой категории:");
                                        var categoryNameTempOld = "";
                                        var categoryNameTempNew = "";
                                        categoryNameTempOld = Console.ReadLine();
                                        Console.WriteLine("Введите новое название категории:");
                                        categoryNameTempNew = Console.ReadLine();
                                        bool resEditCategory = category.EditCategory(categoryNameTempOld,categoryNameTempNew);
                                        if (resEditCategory == true)
                                            Console.WriteLine($"Категория с именем \"{categoryNameTempOld}\" изменена на имя \"{categoryNameTempNew}\"!");
                                        else
                                            Console.WriteLine($"Ошибка, кактегории с именем \"{categoryNameTempOld}\" не существует!");
                                        break;
                                    case "3":
                                        Console.WriteLine("Пункт удаления категории");
                                        Console.WriteLine("Введите название категории, которую хотите удалить:");
                                        var categoryNameTempDelete = "";
                                        categoryNameTempDelete = Console.ReadLine();
                                        bool resDeleteCategory = category.DeleteCategory(categoryNameTempDelete);
                                        if (resDeleteCategory == true)
                                            Console.WriteLine($"Категория с именем \"{categoryNameTempDelete}\" удалена!");
                                        else
                                            Console.WriteLine($"Ошибка, кактегории с именем \"{categoryNameTempDelete}\" не существует!");
                                        break;
                                    case "4":
                                        Console.WriteLine("Пункт добавления позиции");
                                        Console.WriteLine("Выберите категорию из имеющихся: ");
                                        List<Category> allCategories = Category.GetAllCategories();
                                        foreach (Category c in allCategories)
                                        {
                                            Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                                        }
                                        Console.WriteLine("Введите имя категории, в которую хотите добавить позицию: ");
                                        var categoryChoose = Console.ReadLine();
                                        var categoryFind = allCategories.Find(c=>c.Name==categoryChoose);
                                        if (categoryFind != null)
                                        {
                                            Console.WriteLine($"Вы выбрали категорию {categoryFind.Name}");
                                            Console.WriteLine("Введите имя позиции");
                                            var positionName = "";
                                            positionName = Console.ReadLine();
                                            Console.WriteLine("Введите стоимость позиции (валюта - доллары США)");
                                            float positionCost = 0;
                                            positionCost = float.Parse(Console.ReadLine());
                                            position.CreatePosition(positionName,positionCost,categoryFind.Name);
                                            Console.WriteLine("Вы создали новую позицию!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ошибка, такой категории нет!");    
                                        }
                                        break;
                                    case "5":
                                        Console.WriteLine("Пункт обновления позиции");
                                        Console.WriteLine("Выберите категорию из имеющихся: ");
                                        List<Category>  allCatForUpdate= Category.GetAllCategories();
                                        foreach (Category c in allCatForUpdate)
                                        {
                                            Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                                        }
                                        Console.WriteLine("Введите имя категории, в которой хотите изменить позицию: ");
                                        var categoryChooseForUpdate = Console.ReadLine();
                                        var categoryFindForUpdate = allCatForUpdate.Find(c=>c.Name==categoryChooseForUpdate);
                                        if (categoryFindForUpdate != null)
                                        {
                                            Console.WriteLine($"Вы выбрали категорию {categoryFindForUpdate.Name}");
                                            Console.WriteLine($"Все позиции в данной категории:");
                                            List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForUpdate.Name);
                                            foreach (Position p in allPositionsInCategory )
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                            Console.WriteLine("Введите имя позиции, которую хотите изменить");
                                            var positionNameOld = "";
                                            positionNameOld = Console.ReadLine();
                                            Console.WriteLine("Введите новое имя позиции: ");
                                            var positionNameNew = Console.ReadLine() ;
                                            Console.WriteLine("Введите новую стоимость позиции (валюта - доллары США)");
                                            float positionCostNew = 0;
                                            positionCostNew = float.Parse(Console.ReadLine());
                                            bool resPositionUpdate;
                                            resPositionUpdate=position.EditPosition(positionNameOld,positionNameNew,positionCostNew);
                                            if (resPositionUpdate==true)
                                            {
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
                                        break;
                                    case "6":
                                        Console.WriteLine("Пункт удаления позиции");
                                        Console.WriteLine("Выберите категорию из имеющихся: ");
                                        List<Category> allCatForDeletePosition = Category.GetAllCategories();
                                        foreach (Category c in allCatForDeletePosition)
                                        {
                                            Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                                        }
                                        Console.WriteLine("Введите имя категории, в которой хотите удалить позицию: ");
                                        var categoryChooseForDeletePosition = Console.ReadLine();
                                        var categoryFindForDelete = allCatForDeletePosition.Find(c => c.Name == categoryChooseForDeletePosition);
                                        if (categoryFindForDelete != null)
                                        {
                                            Console.WriteLine($"Вы выбрали категорию {categoryFindForDelete.Name}");
                                            Console.WriteLine($"Все позиции в данной категории:");
                                            List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForDelete.Name);
                                            foreach (Position p in allPositionsInCategory)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                            Console.WriteLine("Введите имя позиции, которую хотите удалить");
                                            var positionNameDelete = "";
                                            positionNameDelete = Console.ReadLine();
                                            bool resPositionDelete;
                                            resPositionDelete = position.DeletePosition(positionNameDelete);
                                            if (resPositionDelete==true)
                                            {
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
                                        break;
                                    // Просто список заказов
                                    case "7":
                                        Console.WriteLine("Пункт истории заказов");
                                        Console.WriteLine("Список всех заказов:");
                                        List<Order> allOrders = Order.GetAllOrders();
                                        foreach (Order o in allOrders)
                                        {
                                            Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                                            Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                                            List<Position> posInOrder = o.PositionsInOrder;
                                            foreach (Position p in posInOrder)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                        }
                                        break;
                                    case "8":
                                        Console.WriteLine("Пункт ассортимента доступной продукции:");
                                        Console.WriteLine("Выберите категорию из имеющихся: ");
                                        List<Category> assOfCategory = Category.GetAllCategories();
                                        foreach (Category c in assOfCategory)
                                        {
                                            Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                                        }
                                        Console.WriteLine("Введите имя категории, в которой хотите посмотреть позиции: ");
                                        var categoryChooseForAllPositions = Console.ReadLine();
                                        var categoryFindForAllPositions = assOfCategory.Find(c => c.Name == categoryChooseForAllPositions);
                                        if (categoryFindForAllPositions != null)
                                        {
                                            Console.WriteLine($"Вы выбрали категорию {categoryFindForAllPositions.Name}");
                                            Console.WriteLine($"Все позиции в данной категории:");
                                            List<Position> allPositionsInCategory = Position.GetAllPositionsInCategory(categoryFindForAllPositions.Name);
                                            foreach (Position p in allPositionsInCategory)
                                            {
                                                Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.Category}\", id: \"{p.Id}\"");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ошибка, такой категории нет!");
                                        }
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
                    break;
                case "3":
                    // Получение списка всех пользователей
                    List<User> allUsers = User.GetAllUsers();
                    // Вывод списка пользователей на консоль
                    Console.WriteLine("Список пользователей:");
                    foreach (User u in allUsers)
                    {
                        Console.WriteLine($"Имя пользователя: {u.Login}, Роль: {u.Role}");
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
