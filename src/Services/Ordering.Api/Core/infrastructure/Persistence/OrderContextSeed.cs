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
        public async static Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> Logger)
        {
            if (!orderContext.orders.Any())
            {
             orderContext.orders.AddRange();
                await orderContext.SaveChangesAsync();
            }
        }
        private static IEnumerable<Order> GetPreConfigOrder()
        {
            return new List<Order>
            {
                new Order(){ UserName="swn",FirstName="Mehment",LastName="ozkaya",EmailAddress="ezozo@gmail.com",Country="turkey",TotalPrice=350}
            };
        }
    }
}
