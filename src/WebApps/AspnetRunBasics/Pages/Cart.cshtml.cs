using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {

        private readonly IBasketServices _BasketServices;

        public CartModel(IBasketServices basketServices)
        {
            _BasketServices = basketServices ?? throw new ArgumentNullException(nameof(basketServices));
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var Usenmae = "swn";
            var basket = await _BasketServices.GetBasket(Usenmae);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid ProductId)
        {
            var UserName = "swn";
            var basket= await _BasketServices.GetBasket(UserName);
            var item = basket.Items.Single(x => x.ProductId==ProductId);
            basket.Items.Remove(item);
            var basketupdate = await _BasketServices.UpdateBasket(basket);
            return RedirectToPage();
        }
    }
}