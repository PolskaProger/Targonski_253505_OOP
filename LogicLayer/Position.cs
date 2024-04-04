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

        public static List<Position> positions = new List<Position>();

        public Position CreatePosition(string name, float cost, string category)
        {
            var newPosition = new Position { Name = name, Cost = cost, Category = category, Id = Guid.NewGuid().ToString() };
            positions.Add(newPosition);
            return newPosition;
        }

        public bool EditPosition(string oldName, string newName, float newCost)
        {
            // Assuming 'positions' is a list of Position objects
            var position = positions.Find(p => p.Name == oldName);
            if (position != null)
            {
                position.Name = newName;
                position.Cost = newCost;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePosition(string name)
        {
            // Assuming 'positions' is a list of Position objects
            
            var position = positions.Find(p => p.Name == name);
            if (position != null)
            {
                positions.Remove(position);
                return true;
            }
            else
            { 
                return false;
            }
        }
        public static List<Position> GetAllPositionsInCategory(string category)
        {
            var positionInCategory =  positions.FindAll(p => p.Category == category);
            return positionInCategory;
        }
    }
}
