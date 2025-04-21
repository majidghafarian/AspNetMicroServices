using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Net;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService= orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("{UserName}", Name = "GetShpping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShpping(string UserName)
        {
            var Basket = await _basketService.GetBasket(UserName);
            foreach (var item in Basket.Items)
            {
                var Product = await _catalogService.GetCatalog(item.ProduuctId);

                item.ProductName = Product.Name;
                item.Category = Product.Category;
                item.Summary = Product.Summary;
                item.Description = Product.Description;
                item.ImageFile = Product.ImageFile;
            }
            var order = await _orderService.GetOrdersByUserName(UserName);
            var ShoppingModel = new ShoppingModel
            {
                UserName= UserName,
                Orders = order,
                BasketWithProducts= Basket
            };
            return Ok(ShoppingModel);
        }
    }
}
