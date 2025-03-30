using Discount.api.Entities;
using Discount.api.Repostories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountController> _logger;
        public DiscountController(IDiscountRepository discountRepository, ILogger<DiscountController> logger)
        {
            _discountRepository = discountRepository;
            _logger=logger;
        }

        [HttpGet("GetDiscount/{ProductName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string ProductName)
        {
            _logger.LogInformation("test");
            var coupon = await _discountRepository.GetDiscount(ProductName);
            return Ok(coupon);
        }

        [HttpPost("AddDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddDiscount([FromBody] Coupon coupon)
        {
            if (coupon == null)
            {
                return BadRequest("Product is null");
            }

            await _discountRepository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productname=coupon.ProductName }, coupon );
        }

        [HttpPut("UpdateCoupon")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCoupon([FromBody] Coupon coupon)
        {
            if (coupon == null)
            {
                return BadRequest("Product is null");
            }

            var success = await _discountRepository.UpdateDiscount(coupon);
            if (success)
            {
                return Ok(coupon);
            }
            return NotFound($"Product with id {coupon.Id} not found");
        }

        [HttpDelete("DeleteCoupon/{id}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCoupon(string id)
        {
            var success = await _discountRepository.DeleteDiscount(id);
            if (success)
            {
                return Ok(success);
            }
            return NotFound($"Product with id {id} not found");
        }


    }
}
