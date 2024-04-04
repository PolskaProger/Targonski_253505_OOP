using MongoDB.Driver;
using SceletonOfProj_OOP_.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class UserRep : IRepository<User>
    {
        private DataStorage _storage;
        private readonly string _collectionName = "users";

        public UserRep(DataStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<User> GetAll()
        {
            var filter = Builders<User>.Filter.Empty; // Фильтр для получения всех документов
            return _storage.ReadData<User>(_collectionName, filter);
        }

        public User GetById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id); // Фильтр для поиска по ID
            return _storage.ReadData<User>(_collectionName, filter).FirstOrDefault();
        }

        public void Add(User user)
        {
            _storage.WriteData(_collectionName, user);
        }

        public void Update(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id); // Фильтр для поиска по ID
            var update = Builders<User>.Update
                .Set(u => u.Login, user.Login)
                .Set(u => u.PasswordHash, user.PasswordHash)
                .Set(u => u.Salt, user.Salt)
                .Set(u => u.Role, user.Role); // Обновление полей пользователя
            _storage.UpdateData(_collectionName, filter, update);
        }

        public void Delete(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id); // Фильтр для поиска по ID
            _storage.DeleteData<User>(_collectionName, filter);
        }
    }
}
