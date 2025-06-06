﻿using Discount.Grpc.Entities;

namespace Discount.Grpc.Repostories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string ProductName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string ProductName);
    }
}
