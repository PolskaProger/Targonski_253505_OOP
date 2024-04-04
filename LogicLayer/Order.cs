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
        public List<Position> PositionsInOrder { get; set; }
        public float TotalCost { get; set; }
        public DateTime Date { get; set; }
        public string Id { get; set; }

        public static List<Order> orders = new List<Order>();
        public Order CreateOrder(string customerName, string contact, DateTime dateOfOrder, List<Position> positions)
        {
            var totalCost = 0f;
            foreach (var position in positions)
            {
                totalCost += position.Cost;
            }

            var newOrder = new Order
            {
                CustomerName = customerName,
                Contact = contact,
                PositionsInOrder = positions,
                TotalCost = totalCost,
                Date = dateOfOrder,
                Id = Guid.NewGuid().ToString()
            };
            orders.Add(newOrder);
            return newOrder;
            
        }

        public bool EditOrder(string oldContact, string newCustomerName, string newContact, List<Position> newPositions)
        {
            // Assuming 'orders' is a list of Order objects
            var order = orders.Find(o => o.Contact == oldContact);
            if (order != null)
            {
                order.CustomerName = newCustomerName;
                order.Contact = newContact;
                order.PositionsInOrder = newPositions;
                order.TotalCost = 0;
                foreach (var position in newPositions)
                {
                    order.TotalCost += position.Cost;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteOrder(string contactInfoForDel)
        {
            var orderToDel = orders.Find(o => o.Contact == contactInfoForDel);
            if (orderToDel != null)
            {
                orders.Remove(orderToDel);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Position> GetAllPosOfOrder(string contact)
        {
            var order = orders.Find(o => o.Contact == contact);
            return order.PositionsInOrder;
        }
        public static List<Order> GetAllOrders()
        {
            return orders;
        }
          
    }

}
