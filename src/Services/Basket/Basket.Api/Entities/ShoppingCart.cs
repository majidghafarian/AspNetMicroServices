namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart()
        {

        }
        public ShoppingCart(string _username)
        {
            UserName = _username;
        }
        public decimal TotalPrice => Items.Sum(x => x.Price*x.Quantity);
    }
}
