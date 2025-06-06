﻿using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    public class DiscountProfile:AutoMapper.Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();  
            CreateMap<CouponModel,Coupon>().ReverseMap();

        }
    }
}
