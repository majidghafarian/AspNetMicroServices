using System;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly ICatalogService _CatalogService;
        private readonly IBasketServices _BasketServices;

        public CheckOutModel(ICatalogService catalogService, IBasketServices basketServices)
        {
            _CatalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _BasketServices = basketServices ?? throw new ArgumentNullException(nameof(basketServices));
        }

        [BindProperty]
        public BasketCheckOutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var username = "swn";
            Cart = await _BasketServices.GetBasket(username);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var username = "swn";
            Cart = await _BasketServices.GetBasket(username);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = username;
            Order.TotalPrice = Cart.TotalPrice;

            await _BasketServices.CheckOutBasket(Order);
            
            
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}