﻿namespace Shopping.Aggregator.Models
{
    public class CatalogModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Category { get; set; } = new();
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }

}
