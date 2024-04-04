using System;
using System.Collections.Generic;
using System.Linq;

namespace SceletonOfProj_OOP_.LogicLayer
{
    public class AnalyticsService
    {
        public float CalculateAverageCheck()
        {
            return Order.orders.Any() ? Order.orders.Average(order => order.TotalCost) : 0;
        }

        public float CalculateMonthlyAverageCheck(int year, int month)
        {
            var monthlyOrders = Order.orders.Where(order => order.Date.Year == year && order.Date.Month == month).ToList();
            return monthlyOrders.Any() ? monthlyOrders.Average(order => order.TotalCost) : 0;
        }

        public (string CustomerName, string Contact) GetMostValuableCustomer()
        {
            var customer = Order.orders
                .GroupBy(order => new { order.CustomerName, order.Contact })
                .Select(group => new { group.Key, TotalCost = group.Sum(order => order.TotalCost) })
                .OrderByDescending(customer => customer.TotalCost)
                .FirstOrDefault();
            return customer != null ? (customer.Key.CustomerName, customer.Key.Contact) : ("", "");
        }

        public Position GetMostPopularPosition()
        {
            var position = Order.orders
                .SelectMany(order => order.PositionsInOrder)
                .GroupBy(position => position)
                .Select(group => new { Position = group.Key, Count = group.Count() })
                .OrderByDescending(result => result.Count)
                .FirstOrDefault();
            return position?.Position;
        }
    }
}
