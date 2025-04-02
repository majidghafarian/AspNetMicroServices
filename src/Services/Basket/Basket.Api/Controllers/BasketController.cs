using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repostories;
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
        public BasketController(IbasketRepository Repository,DiscountGrpcServices discountGrpcServices)
        {
            _Repository = Repository;
            _discountGrpcServices = discountGrpcServices;
        }

        [HttpGet("GEtBasket")]
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
           var Coupon=   await  _discountGrpcServices.GetDiscount(item.ProductName);
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

    }
}
