using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;
        public OrderService(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public Task<OrderResponseModel> GetOrderById(string userName, string orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"api/Order/GetOrdersByUserName/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
