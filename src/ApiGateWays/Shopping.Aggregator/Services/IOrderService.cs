using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
        Task<OrderResponseModel> GetOrderById(string userName, string orderId);
  
    }
}
