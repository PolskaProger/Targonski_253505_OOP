using SceletonOfProj_OOP_.LogicLayer;
using SceletonOfProj_OOP_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.ForAllUsers
{
    public class Registration
    {
        public static void RegistrationMenu(CorrectInputService correctInput,bool exitMenuReg, User user, UserRep userRep)
        {
            while (!exitMenuReg)
            {
                Console.WriteLine("Меню регистрации пользователя:");
                Console.WriteLine("Введите имя пользователя:");
                string userNameTemp = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                string passwordTemp = Console.ReadLine();
                var password_is_correct=correctInput.ValidatePassword(passwordTemp);
                if (password_is_correct == false)
                {
                    Console.WriteLine("Ошибка, пароль не является валидным! (больше 8 символов, 1 большая и одна прописная латинская буква)");
                }
                else
                {
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
                    var newUser = user.Register(userNameTemp, passwordTemp, roleTemp);
                    userRep.Add(newUser);
                    Console.WriteLine($"Пользователь под именем {userNameTemp} успешно создан и добавлен в БД!");
                    Console.WriteLine("Хотите создать нового пользователя?");
                    Console.WriteLine("1.Да");
                    Console.WriteLine("2.Нет");
                    var choiceMenuReg = Console.ReadLine();
                    switch (choiceMenuReg)
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
            }
        }
    }
}
