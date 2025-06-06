﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommand: IRequest
    {
        public int Id { get; set; }
        public DeleteOrderCommand(int _id)
        {
            Id=_id;
        }
    }
    
}
