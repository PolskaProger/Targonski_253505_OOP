using MongoDB.Driver;
using SceletonOfProj_OOP_.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class OrderRep : IRepository<Order>
    {
        private DataStorage _storage;
        private readonly string _collectionName = "orders";

        public OrderRep(DataStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<Order> GetAll()
        {
            var filter = Builders<Order>.Filter.Empty; // Фильтр для получения всех документов
            return _storage.ReadData<Order>(_collectionName, filter);
        }

        public Order GetById(string id)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id); // Фильтр для поиска по ID
            return _storage.ReadData<Order>(_collectionName, filter).FirstOrDefault();
        }

        public void Add(Order order)
        {
            _storage.WriteData(_collectionName, order);
        }

        public void Update(Order order)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, order.Id); // Фильтр для поиска по ID
            var update = Builders<Order>.Update
                .Set(o => o.CustomerName, order.CustomerName)
                .Set(o => o.Contact, order.Contact)
                .Set(o => o.PositionsInOrder, order.PositionsInOrder)
                .Set(o => o.TotalCost, order.TotalCost)
                .Set(o => o.Date, order.Date); // Обновление полей заказа

            _storage.UpdateData(_collectionName, filter, update);
        }

        public void Delete(string id)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id); // Фильтр для поиска по ID
            _storage.DeleteData<Order>(_collectionName, filter);
        }
    }
}
