using Catalog.Api.Entities;
using Microsoft.OpenApi.Validations;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
                Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
                CatalogContextseed.SeedData(Products);
            }
            catch (Exception ex)
            {
                // لاگ کردن یا مدیریت خطا
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
                throw;
            }

        }
        public IMongoCollection<Product> Products { get; }
    }
}
