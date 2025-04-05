using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandHandler:IRequestHandler<CheckOutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckOutOrderCommandHandler> _logger;
        public CheckOutOrderCommandHandler(IOrderRepository orderRepository,IMapper mapper,IEmailService emailService ,ILogger<CheckOutOrderCommandHandler> logger)
        {
            _orderRepository =orderRepository;
            _mapper = mapper;
            _emailService=emailService; 
            _logger = logger;
        }
        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderentity=_mapper.Map<Order>(request);
            var neworder =await _orderRepository.AddAsync(orderentity);
            _logger.LogInformation($"Order {orderentity.Id} is successfully created.");
            //send email
            await SendMail(neworder);
            return neworder.Id;

        }
        private async Task SendMail(Order order)
        {
            var email = new Email
            {
                To = order.EmailAddress , // فرض بر اینکه داخل Order یک فیلد به اسم CustomerEmail داری
                Subject = "Order Created",
                Body = $"Your order #{order.Id} has been successfully created."
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Order {order.Id} failed due to an error with the email service {ex.Message}");
            }
       
        }
    }
    
    
}
