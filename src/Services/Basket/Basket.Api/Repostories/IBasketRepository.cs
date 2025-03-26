using Basket.Api.Entities;

namespace Basket.Api.Repostories
{
    public interface IbasketRepository
    {
        Task<ShoppingCart> GetBasket(string UserName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
        Task DeleteBasket(string UserName);
    }
}
