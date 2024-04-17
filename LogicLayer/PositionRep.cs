using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    using MongoDB.Driver;
    using SceletonOfProj_OOP_.DataLayer;
    using System.Collections.Generic;
    using System.Linq;

    public class PositionRep : IRepository<Position>
    {
        private DataStorage _storage;
        private readonly string _collectionName = "positions";

        public PositionRep(DataStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<Position> GetAll()
        {
            var filter = Builders<Position>.Filter.Empty; // Фильтр для получения всех документов
            return _storage.ReadData<Position>(_collectionName, filter);
        }

        public Position GetById(string id)
        {
            var filter = Builders<Position>.Filter.Eq(p => p.Id, id); // Фильтр для поиска по ID
            return _storage.ReadData<Position>(_collectionName, filter).FirstOrDefault();
        }

        public void Add(Position position)
        {
            _storage.WriteData(_collectionName, position);
        }

        public void Update(Position position)
        {
            var filter = Builders<Position>.Filter.Eq(p => p.Id, position.Id); // Фильтр для поиска по ID
            var update = Builders<Position>.Update
                .Set(p => p.Name, position.Name)
                .Set(p => p.Cost, position.Cost)
                .Set(p => p.category, position.category); // Обновление полей позиции

            _storage.UpdateData(_collectionName, filter, update);
        }

        public void Delete(string name)
        {
            var filter = Builders<Position>.Filter.Eq(p => p.Name, name); // Фильтр для поиска по ID
            _storage.DeleteData<Position>(_collectionName, filter);
        }
    }

}
