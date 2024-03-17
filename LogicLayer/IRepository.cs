using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Add(T item);
        void Update(T item);
        void Delete(string id);
    }
}
