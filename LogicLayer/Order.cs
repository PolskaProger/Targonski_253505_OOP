using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class Order
    {
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public List<Position> Positions { get; set; }
        public float TotalCost { get; set; }
        public DateTime Date { get; set; }
        public string Id { get; set; }

        private static List<Order> orders = new List<Order>();
        public Order CreateOrder(string customerName, string contact, List<Position> positions)
        {
            var totalCost = 0f;
            foreach (var position in positions)
            {
                totalCost += position.Cost;
            }

            return new Order
            {
                CustomerName = customerName,
                Contact = contact,
                Positions = positions,
                TotalCost = totalCost,
                Date = DateTime.Now,
                Id = Guid.NewGuid().ToString()
            };
        }

        public void EditOrder(string id, string newCustomerName, string newContact, List<Position> newPositions)
        {
            // Assuming 'orders' is a list of Order objects
            var order = orders.Find(o => o.Id == id);
            if (order != null)
            {
                order.CustomerName = newCustomerName;
                order.Contact = newContact;
                order.Positions = newPositions;
                order.TotalCost = 0;
                foreach (var position in newPositions)
                {
                    order.TotalCost += position.Cost;
                }
            }
        }

        public void DeleteOrder(string id)
        {
            // Assuming 'orders' is a list of Order objects
            orders.RemoveAll(o => o.Id == id);
        }
    }

}
