using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
 using Ordering.Application.Features.Commands.CheckOutOrder;

namespace Ordering.Api.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly MediatR.IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        public BasketCheckoutConsumer(ILogger<BasketCheckoutConsumer> logger,IMapper mapper, MediatR.IMediator mediator)
        {
            _mediator=mediator;
            _mapper=mapper;
            _logger=logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
           var Conmmand=_mapper.Map<CheckOutOrderCommand>(context.Message);
            Conmmand.UserName = context.Message.UserName;
            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order for UserName: {newUserName}", Conmmand.UserName);
           var res= await _mediator.Send(Conmmand);
        }
    }
}
