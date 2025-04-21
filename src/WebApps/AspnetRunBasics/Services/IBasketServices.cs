using AspnetRunBasics.Models;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface IBasketServices
    {
        Task<BasketModel> GetBasket(string UserName);
        Task<BasketModel> UpdateBasket(BasketModel basket);
        Task CheckOutBasket(BasketCheckOutModel basketCheckOut);
    }
}
