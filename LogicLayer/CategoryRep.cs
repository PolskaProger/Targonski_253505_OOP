using MongoDB.Driver;
using SceletonOfProj_OOP_.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class CategoryRep : IRepository<Category>
    {
        public DataStorage _storage;
        public readonly string _collectionName = "categories";

        public CategoryRep(DataStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<Category> GetAll()
        {
            var filter = Builders<Category>.Filter.Empty; // Фильтр для получения всех документов
            return _storage.ReadData<Category>(_collectionName, filter);
        }

        public Category GetById(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id); // Фильтр для поиска по ID
            return _storage.ReadData<Category>(_collectionName, filter).FirstOrDefault();
        }

        public void Add(Category category)
        {
            _storage.WriteData(_collectionName, category);
        }

        public void Update(Category category)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, category.Id); // Фильтр для поиска по ID
            var update = Builders<Category>.Update
                .Set(c => c.Name, category.Name); // Обновление имени категории

            _storage.UpdateData(_collectionName, filter, update);
        }

        public void Delete(string name)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Name, name); // Фильтр для поиска по ID
            _storage.DeleteData<Category>(_collectionName, filter);
        }
    }
}
