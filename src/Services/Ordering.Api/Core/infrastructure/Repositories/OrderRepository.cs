using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.infrastructure.Repositories
{
    public class OrderRepository: RepositoryBase<Order>, IOrderRepository
    {
        private readonly OrderContext _context;
        public OrderRepository(OrderContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderlist=await _context.orders.
                Where(o => o.UserName == userName)
                .ToListAsync();
            return  orderlist;
        }
   
    }
}
