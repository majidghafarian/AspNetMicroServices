namespace Shopping.Aggregator.Models
{
    public class BasketItemExtendedModel
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public Guid ProduuctId { get; set; }

        ////product realated additional fields
        public List<string> Category { get; set; } = new();
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }

    }
}
