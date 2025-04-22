using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogService _CatalogService;
        private readonly IBasketServices _BasketServices;

        public ProductModel(ICatalogService catalogService, IBasketServices basketServices)
        {
            _CatalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _BasketServices = basketServices ?? throw new ArgumentNullException(nameof(basketServices));
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryname)
        {
          var  productlist = await _CatalogService.GetCatalog();
            CategoryList=productlist.Select(c => c.Category).Distinct();
            if (!string.IsNullOrWhiteSpace(categoryname))
            {
                ProductList =  productlist.Where( p=>p.Category==categoryname);
                SelectedCategory =categoryname;
            }
            else
            {
                ProductList = productlist;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            //if (!User.Identity.IsAuthenticated)
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