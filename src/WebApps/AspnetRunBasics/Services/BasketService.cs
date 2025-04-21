using AspnetRunBasics.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketServices
    {
        private readonly HttpClient _client;
        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public Task CheckOutBasket(BasketCheckOutModel basketCheckOut)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketModel> GetBasket(string UserName)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketModel> UpdateBasket(BasketModel basket)
        {
            throw new System.NotImplementedException();
        }
    }
}
