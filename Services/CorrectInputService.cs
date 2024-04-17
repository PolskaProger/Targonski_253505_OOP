using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.Services
{
    public class CorrectInputService
    {
        // Метод для валидации почты
        public bool ValidateEmail(string email)
        {
            // Проверяем, что почта соответствует формату @gmail.com, @mail.ru или @yandex.by
            string pattern = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|mail\.ru|yandex\.by)$";
            return Regex.IsMatch(email, pattern);
        }

        // Метод для валидации пароля
        public bool ValidatePassword(string password)
        {
            // Проверяем, что пароль больше 8 символов и содержит хотя бы одну строчную и одну прописную букву
            return password.Length >= 8 && Regex.IsMatch(password, @"[a-z]") && Regex.IsMatch(password, @"[A-Z]");
        }

        // Метод для валидации float
        public bool ValidateFloat(string input)
        {
            float result;
            return float.TryParse(input, out result);
        }

        // Метод для валидации int
        public bool ValidateInt(string input)
        {
            int result;
            return int.TryParse(input, out result);
        }
    }
}
