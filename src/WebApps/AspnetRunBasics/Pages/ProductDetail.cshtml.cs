using System;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogService _CatalogService;
        private readonly IBasketServices _BasketServices;

        public ProductDetailModel(ICatalogService catalogService, IBasketServices basketServices)
        {
            _CatalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _BasketServices = basketServices ?? throw new ArgumentNullException(nameof(basketServices));
        }


        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _CatalogService.GetCatalog(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {        //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });
            var product = await _CatalogService.GetCatalog(productId);
            var UserName = "swn";
            var basket = await _BasketServices.GetBasket(UserName);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Quintity = 1,
                Color = "Black",
                Price = product.Price
            });
            var updatedBasket = await _BasketServices.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}