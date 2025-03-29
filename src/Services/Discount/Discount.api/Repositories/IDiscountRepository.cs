using Discount.api.Entities;

namespace Discount.api.Repostories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string ProductName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string ProductName);
    }
}
