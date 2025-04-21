using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string Category);
        Task<CatalogModel> GetCatalog(Guid id);
        Task<CatalogModel> CreateCatalog(CatalogModel catalog);
    }
}
