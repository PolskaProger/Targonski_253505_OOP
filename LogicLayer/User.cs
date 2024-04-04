using SceletonOfProj_OOP_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash;
        public byte[] Salt;
        public string Role { get; set; }
        public string Id { get; set; }

        public static List<User> users = new List<User>();
        public bool Authorize(string login, string password)
        {
            var user = users.Find(u => u.Login == login);
            if (user != null)
            {
                return user.CheckPassword(login,password, user.Salt);
            }
            return false;
        }
        public string RoleOfUser(string login)
        {
            var user = users.Find(u => u.Login == login);
            return user.Role;
        }

        public User Register(string login, string password, string role)
        {
            var (passwordHash,salt) = SetPassword(password);
            var newUser = new User { Login = login, PasswordHash = passwordHash,Salt = salt ,Role = role, Id = Guid.NewGuid().ToString() };
            users.Add(newUser);
            return newUser;
        }
        // Метод для установки пароля
        public (string PasswordHash, byte[] salt) SetPassword(string password)
        {
            byte[] salt = AuthService.GenerateSalt(16); // Генерация соли
            PasswordHash = AuthService.HashPassword(password, salt); // Хеширование пароля
            return (PasswordHash, salt);
        }

        // Метод для проверки пароля
        public bool CheckPassword(string Login,string password, byte[] salt )
        {
            byte[] saltTemp = salt;
            return AuthService.ValidatePassword(password, PasswordHash, salt);
        }
        public static List<User> GetAllUsers()
        {
            return users;
        }
    }
}
