using PolicyBasedAuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBasedAuthenticationDemo.Data
{
    public class Orders
    {
        static List<Order> orders = new List<Order>
        {
            new Order { Id = 1, UserId = 1, Date = DateTime.UtcNow, Total = 100M },
            new Order { Id = 2, UserId = 1, Date = DateTime.UtcNow, Total = 20M },
            new Order { Id = 3, UserId = 2, Date = DateTime.UtcNow, Total = 400M },
            new Order { Id = 4, UserId = 3, Date = DateTime.UtcNow, Total = 500M },
        };

        public static IQueryable<Order> GetAll()
        {
            return orders.AsQueryable();
        }
        public static IQueryable<Order> GetByUser(int userId)
        {
            return orders.Where(o => o.UserId == userId).AsQueryable();
        }
        public static Order Get(int id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }
    }
}
