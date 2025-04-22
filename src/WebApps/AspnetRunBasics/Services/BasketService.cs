using AspnetRunBasics.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using AspnetRunBasics.Services.Extensions;
using System.Data;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketServices
    {
        private readonly HttpClient _client;
        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task CheckOutBasket(BasketCheckOutModel basketCheckOut)
        {
            var response = await _client.PostAsJson($"/basket/checkout", basketCheckOut);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Something went wrong when calling API: {response.StatusCode}");
            }
          
        }

        public async Task<BasketModel> GetBasket(string UserName)
        {
            var response = await _client.GetAsync($"/Basket/{UserName}");
            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel basket)
        {
            var response = await _client.PostAsJson($"/basket",basket);
            if (!response.IsSuccessStatusCode)
            { 
            throw new Exception($"Something went wrong when calling API: {response.StatusCode}");
            }
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
