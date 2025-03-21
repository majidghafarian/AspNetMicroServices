using Catalog.Api.Entities;
using Microsoft.OpenApi.Validations;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = Client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products =database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextseed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
