using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequest<UpdateOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly Logger<UpdateOrderCommandHandler> _logger;
        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, Logger<UpdateOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var oreder =await _orderRepository.GetByIdAsync(request.Id);
            if (oreder == null)
            {
                _logger.LogError($"Order with id {request.Id} not found.");
                throw new NotFoundException(nameof(Order), request.Id);
            }
           _mapper.Map (request,oreder,typeof(UpdateOrderCommand),typeof(Order));
           await _orderRepository.UpdateAsync(oreder);   
            _logger.LogInformation($"Order {oreder.Id} is successfully updated.");
            return Unit.Value;

        }
    }
}
