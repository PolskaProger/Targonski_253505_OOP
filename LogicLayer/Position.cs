using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class Position
    {
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }

        private static List<Position> positions = new List<Position>();

        public Position CreatePosition(string name, float cost, string category)
        {
            return new Position { Name = name, Cost = cost, Category = category, Id = Guid.NewGuid().ToString() };
        }

        public void EditPosition(string id, string newName, float newCost, string newCategory)
        {
            // Assuming 'positions' is a list of Position objects
            var position = positions.Find(p => p.Id == id);
            if (position != null)
            {
                position.Name = newName;
                position.Cost = newCost;
                position.Category = newCategory;
            }
        }

        public void DeletePosition(string id)
        {
            // Assuming 'positions' is a list of Position objects
            positions.RemoveAll(p => p.Id == id);
        }
    }
}
