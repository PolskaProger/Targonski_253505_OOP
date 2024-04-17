using SceletonOfProj_OOP_.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceletonOfProj_OOP_.UseCases.Analytic
{
    public class Analytic
    {
        public static void AnalyticMenu()
        {
            Console.WriteLine("Пункт аналитики:");
            var analyticsService = new AnalyticsService();
            var averageCheck = analyticsService.CalculateAverageCheck();
            Console.WriteLine($"Средний чек по всем заказам: {averageCheck}$");
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var monthlyAverageCheck = analyticsService.CalculateMonthlyAverageCheck(year, month);
            Console.WriteLine($"Средний чек за текущий месяц: {monthlyAverageCheck}$");
            var (CustomerName, Contact) = analyticsService.GetMostValuableCustomer();
            Console.WriteLine($"Самый платежеспособный клиент: {CustomerName}, Контакты клиента: {Contact}");
            var mostPopularPosition = analyticsService.GetMostPopularPosition();
            Console.WriteLine($"Самая популярная позиция: {mostPopularPosition?.Name ?? "Нет данных"}");
        }
    }
}
