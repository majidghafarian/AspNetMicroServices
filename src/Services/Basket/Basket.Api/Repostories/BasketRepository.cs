using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.Api.Repostories
{
    public class BasketRepository : IbasketRepository
    {
        private readonly IDistributedCache _RedisCache;
        public BasketRepository(IDistributedCache RedisCache)
        {
            _RedisCache = RedisCache??throw new ArgumentNullException(nameof(RedisCache));
        }
        public async Task DeleteBasket(string UserName)
        {
            await _RedisCache.RemoveAsync(UserName);
        }

        public async Task<ShoppingCart> GetBasket(string UserName)
        {
            // داده‌ها از کش Redis خوانده می‌شود.
            var cachedBasket = await _RedisCache.GetStringAsync(UserName);

            // اگر کش Redis خالی است یا داده‌ها null هستند، یک شیء پیش‌فرض بازگشت داده می‌شود.
            if (string.IsNullOrEmpty(cachedBasket))
            {
                return new ShoppingCart(UserName); // ایجاد و بازگشت یک شیء پیش‌فرض.
            }

            // در غیر این صورت داده‌ها deserialize می‌شوند.
            try
            {
                return JsonConvert.DeserializeObject<ShoppingCart>(cachedBasket);
            }
            catch (JsonException)
            {
                // در صورتی که deserialization شکست بخورد، یک شیء پیش‌فرض باز می‌گردانیم.
                return new ShoppingCart(UserName);
            }
        }


        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
          await _RedisCache.SetStringAsync(shoppingCart.UserName,JsonConvert.SerializeObject(shoppingCart));
            return await  GetBasket(shoppingCart.UserName);
        }
    }
}
