﻿using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Commands.CheckOutOrder;

namespace Ordering.Api.Mapping
{
    public class OrderingProfile:Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckOutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
