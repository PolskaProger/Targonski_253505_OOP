using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.Services
{
    internal class AuthService
    {
        // Метод для генерации случайной соли заданной длины
        public static byte[] GenerateSalt(int length)
        {
            // Создаем объект криптографически безопасного генератора случайных чисел
            using (var rng = new RNGCryptoServiceProvider())
            {
                // Создаем массив байтов заданной длины
                var salt = new byte[length];
                // Заполняем массив случайными байтами
                rng.GetBytes(salt);
                // Возвращаем массив байтов
                return salt;
            }
        }

        public static string HashPassword(string password, byte[] salt)
        {
            using (var sha = new SHA256Managed())
            {
                // Преобразуем пароль в массив байтов
                var passwordBytes = Encoding.UTF8.GetBytes(password);

                // Соединяем массивы байтов пароля и соли
                var passwordAndSaltBytes = new byte[passwordBytes.Length + salt.Length];
                Array.Copy(passwordBytes, 0, passwordAndSaltBytes, 0, passwordBytes.Length);
                Array.Copy(salt, 0, passwordAndSaltBytes, passwordBytes.Length, salt.Length);

                // Вычисляем хеш от соединенных массивов байтов
                var hashBytes = sha.ComputeHash(passwordAndSaltBytes);

                // Преобразуем хеш в строку в шестнадцатеричном формате
                var hashString = BitConverter.ToString(hashBytes).Replace("-", "");

                // Возвращаем строку с хешем
                return hashString;
            }
        }


        // Метод для проверки пароля с хешем и солью
        public static bool ValidatePassword(string password, string hash, byte[] salt)
        {
            // Получаем хеш от введенного пароля с той же солью
            var passwordHash = HashPassword(password, salt);
            // Сравниваем хеши на равенство
            return passwordHash == hash;
        }

    }
}
