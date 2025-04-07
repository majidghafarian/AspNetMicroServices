using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public async static Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.orders.Any())
            {
                orderContext.orders.AddRange(GetPreConfigOrder());
                await orderContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Order> GetPreConfigOrder()
        {
            return new List<Order>
        {
            new Order { UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozo@gmail.com", Country = "Turkey", TotalPrice = 350 }
        };
        }
    }
}
