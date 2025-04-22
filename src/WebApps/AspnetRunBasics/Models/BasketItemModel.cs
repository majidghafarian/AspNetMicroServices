using System;

namespace AspnetRunBasics.Models
{
    public class BasketItemModel
    {
        public int Quintity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
    }
}
