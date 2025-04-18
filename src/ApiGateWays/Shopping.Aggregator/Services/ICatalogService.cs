﻿using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<CatalogModel> GetCatalog(Guid id);
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string categoryName);
        //Task<IEnumerable<CatalogModel>> GetCatalogByName(string name);
    }
}
