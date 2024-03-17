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
        public string Password { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }

        private static List<User> users = new List<User>();
        public bool Authorize(string login, string password)
        {
            // Assuming 'users' is a list of User objects
            var user = users.Find(u => u.Login == login);
            return user != null && user.Password == password;
        }

        public User Register(string login, string password, string role)
        {
            var newUser = new User { Login = login, Password = password, Role = role, Id = Guid.NewGuid().ToString() };
            users.Add(newUser);
            return newUser;
        }
    }
}
