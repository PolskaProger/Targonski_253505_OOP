using SceletonOfProj_OOP_.LogicLayer;
using SceletonOfProj_OOP_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Casher
{
    public class UpdateOrder
    {
        public static void UpdateOrderMenu(CorrectInputService correctInput,bool exitChangePosInOrder, bool exitAddPosInOrder, OrderRep orderRep) 
        {
            Console.WriteLine("Пункт изменения заказа");
            List<Order> orderList = Order.GetAllOrders();
            foreach (Order o in orderList)
            {
                Console.WriteLine($"Имя заказчика: {o.CustomerName}, контактные данные: {o.Contact}, дата заказа: {o.Date.ToString()}, общая стоимость заказа: {o.TotalCost}, id: {o.Id}");
                Console.WriteLine($"Список позиций в заказе на имя {o.CustomerName}");
                List<Position> posInOrderToUpdate = o.PositionsInOrder;
                foreach (Position p in posInOrderToUpdate)
                {
                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", колличество позиции {p.Quantity}, id: \"{p.Id}\"");
                }
            }
            Console.WriteLine("Введите контактные данные заказчика, заказ которого хотите изменить:");
            var contactTemp = Console.ReadLine();
            exitChangePosInOrder = false;
            Order orderToUpdate = orderList.Find(o => o.Contact == contactTemp);
            if (orderToUpdate != null)
            {
                Console.WriteLine("Введите новое имя заказчика:");
                var newCustomerName = Console.ReadLine();
                Console.WriteLine("Введите новые контактные данные");
                var newContactName = Console.ReadLine();
                bool email_is_correct = correctInput.ValidateEmail(newContactName);
                if (email_is_correct == false)
                {
                    Console.WriteLine("Ошибка ввода email! Поддерживаются форматы:...@gmail.com, ...@mail.ru, ...@yandex.by");
                }
                else
                {
                    while (!exitChangePosInOrder)
                    {
                        Console.WriteLine("Меню для изменения позиций в заказе:");
                        Console.WriteLine("Все позиции в заказе на имя");
                        List<Position> posInOrderToUpdateTemp = orderToUpdate.PositionsInOrder;
                        foreach (Position p in posInOrderToUpdateTemp)
                        {
                            Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", колличество позиции {p.Quantity}, id: \"{p.Id}\"");
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
                                            Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", id: \"{p.Id}\"");
                                        }
                                        Console.WriteLine("Выберите имя позиция, которую хотите добавить в заказ:");
                                        var namePositionForOrder = Console.ReadLine();
                                        var choosePositionForOrder = allPositionsInCategory.Find(p => p.Name == namePositionForOrder);
                                        if (choosePositionForOrder != null)
                                        {
                                            int quantityOfPosInOrder = 0;
                                            Console.WriteLine("Введите колличество позиции в заказе:");
                                            quantityOfPosInOrder = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Позиция добавлена в заказ");
                                            choosePositionForOrder.Quantity=quantityOfPosInOrder;
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
                                    Console.WriteLine($" Позиция: \"{p.Name}\", стоимость: {p.Cost}$, категория позиции: \"{p.category}\", id: \"{p.Id}\"");
                                }
                                Console.WriteLine("Введите имя позиции которую хотите удалить из заказа");
                                var nameOfPosToDelFromOrder = Console.ReadLine();
                                var posDelFromOrder = allPosInOrderToDel.Find(p => p.Name == nameOfPosToDelFromOrder);
                                if (posDelFromOrder != null)
                                {
                                    allPosInOrderToDel.RemoveAll(p => p.Name == nameOfPosToDelFromOrder);
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
                    orderRep.Update(orderToUpdate);
                    Console.WriteLine("Заказ успешно обновлён!");
                }
            }
            else
            {
                Console.WriteLine($"Ошибка, заказа на контактные данные {contactTemp} не существует!");
            }
        }
    }
}
