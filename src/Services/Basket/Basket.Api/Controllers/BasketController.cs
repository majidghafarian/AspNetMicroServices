using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repostories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IbasketRepository _Repository;
        private readonly DiscountGrpcServices _discountGrpcServices;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;  
        public BasketController(IbasketRepository Repository, DiscountGrpcServices discountGrpcServices,IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _Repository = Repository;
            _discountGrpcServices = discountGrpcServices;
            _mapper = mapper??throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint=publishEndpoint;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string UserName)
        {
            var Basket = await _Repository.GetBasket(UserName);
            return Ok(Basket ?? new ShoppingCart(UserName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart Basket)
        {
            foreach (var item in Basket.Items)
            {
                var Coupon = await _discountGrpcServices.GetDiscount(item.ProductName);
                item.Price -= Coupon.Amount;
            }
            return Ok(await _Repository.UpdateBasket(Basket));
        }
        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string UserName)
        {
            await _Repository.DeleteBasket(UserName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType( (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckOut([FromBody]BasketCheckOut basket)
        {
            //get existing basket with total price
            var basketFromRepo = await _Repository.GetBasket(basket.UserName);
            if (basketFromRepo == null)
            {
                return BadRequest();
            }
            var evetmessage = _mapper.Map<BasketCheckoutEvent>(basket);
            evetmessage.TotalPrice= basketFromRepo.TotalPrice;
          await  _publishEndpoint.Publish(evetmessage);
            //remove the old basket
            await _Repository.DeleteBasket(basket.UserName);
            //create charge
            //send to rabbitmq
            return Accepted();

        }


    }
}
