using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Commands.CheckOutOrder;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository =orderRepository;
            _mapper = mapper;

            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            // بررسی صحت ورودی
            if (request.Id == null)
            {
                // لاگ خطا
                _logger.LogError($"Request Id Is Required.");
                throw new NotFoundException("Order",request.Id);

            }
            var job = await _orderRepository.GetByIdAsync(request.Id);
            if (job == null)
            {
                _logger.LogError($"Order with id {request.Id} not found.");
                throw new NotFoundException(nameof(Order), request.Id);
            }
            // حذف تسک
            await _orderRepository.DeleteAsync(job);
            _logger.LogInformation($"Order with id {request.Id} deleted successfully.");
            return Unit.Value;
        }

    }
}



