﻿using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;
        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
        {
           var response=await _client.GetAsync($"/orders/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
