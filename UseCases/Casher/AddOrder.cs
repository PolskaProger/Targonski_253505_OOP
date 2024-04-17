using SceletonOfProj_OOP_.LogicLayer;
using SceletonOfProj_OOP_.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Casher
{
    public class AddOrder
    {
        public static void AddOrderMenu(CorrectInputService correctInput,bool exitAddPosInOrder, Order order, OrderRep orderRep)
        {
            exitAddPosInOrder = false;
            Console.WriteLine("Пункт добаления заказа");
            Console.WriteLine("Введите имя заказчика:");
            var customerNameTemp = "";
            customerNameTemp = Console.ReadLine();
            Console.WriteLine("Введите контактную информацию (email):");
            var contactInfoTemp = "";
            contactInfoTemp = Console.ReadLine();
            bool email_is_correct = correctInput.ValidateEmail(contactInfoTemp);
            if (email_is_correct == false)
            {
                Console.WriteLine("Ошибка ввода email! Поддерживаются форматы:...@gmail.com, ...@mail.ru, ...@yandex.by");
            }
            else
            {
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
                List<Position> positionsInOrder = [];
                while (!exitAddPosInOrder)
                {
                    Console.WriteLine("Добавление позиций в заказ");
                    Console.WriteLine("Выберите категорию из имеющихся: ");
                    foreach (Category c in Category.categories)
                    {
                        Console.WriteLine($"Категория: {c.Name}, id: {c.Id}");
                    }
                    Console.WriteLine("Введите имя категории, из которой хотите добавить позицию в заказ: ");
                    var categoryChooseForAllPositions = Console.ReadLine();
                    var categoryFindForAllPositions = Category.categories.Find(c => c.Name == categoryChooseForAllPositions);
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
                            choosePositionForOrder.Quantity = quantityOfPosInOrder;
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
                orderRep.Add(resOrder);
                Console.WriteLine($"Заказ на имя {customerNameTemp} создан! на общую сумму {resOrder.TotalCost} и добавлен в БД$");
            }
        }
    }
}
