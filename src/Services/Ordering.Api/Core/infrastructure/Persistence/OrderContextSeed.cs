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
        new Order
        {
            UserName = "jdoeeeeeeee11",
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "jdoe@example.com",
            AddressLine = "123 Main St",
            Country = "USA",
            State = "CA",
            ZipCode = "90001",
            TotalPrice = 199.99m,
            CardName = "John Doe",
            CardNumber = "4111111111111111",
            Expiration = "12/26",
            CVV = "123",
            PaymentMethod = "CreditCard",
            CreatedBy = "Seed",
            CreatedDate = DateTime.UtcNow,
            LastModifiedBy = "Seed",
            LastModifiedDate = DateTime.UtcNow
        },
        new Order
        {
            UserName = "mary.smith",
            FirstName = "Mary",
            LastName = "Smith",
            EmailAddress = "mary.smith@example.com",
            AddressLine = "456 Oak Avenue",
            Country = "UK",
            State = "London",
            ZipCode = "W1D 3QF",
            TotalPrice = 249.50m,
            CardName = "Mary Smith",
            CardNumber = "5500000000000004",
            Expiration = "10/25",
            CVV = "456",
            PaymentMethod = "MasterCard",
            CreatedBy = "Seed",
            CreatedDate = DateTime.UtcNow,
            LastModifiedBy = "Seed",
            LastModifiedDate = DateTime.UtcNow
        },
        new Order
        {
             UserName = "ali.rahimi",
             FirstName = "Ali",
             LastName = "Rahimi",
             EmailAddress = "ali.rahimi@example.com",
             AddressLine = "بلوار کشاورز، پلاک ۱۵",
             Country = "Iran",
              State = "Tehran",
              ZipCode = "1599773113",
             TotalPrice = 520000m, // مبلغ به تومان فرض شده
             CardName = "Ali Rahimi",
             CardNumber = "6037997512345678",
             Expiration = "01/27",
             CVV = "789",
             PaymentMethod = "MellatCard",
             CreatedBy = "Seed",
             CreatedDate = DateTime.UtcNow,
             LastModifiedBy = "Seed",
             LastModifiedDate = DateTime.UtcNow
     }

    };
        }

    }
}

