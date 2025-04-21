﻿using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<BasketModel> GetBasket(string UserName)
        {
            var response = await _httpClient.GetAsync($"api/v1/Basket/{UserName}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
