using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<CatalogModel> CreateCatalog(CatalogModel catalog)
        {

            var Response = await _client.PostAsJson($"/Catalog/AddProduct",catalog);
            if (Response.IsSuccessStatusCode)
            {
                return await Response.ReadContentAs<CatalogModel>();
            }
            else
            {
                throw new ApplicationException($"Something went wrong when calling the API: {Response.ReasonPhrase}");
            }

        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var Response = await _client.GetAsync("/Catalog/GetAllProducts");
            return await Response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(Guid id)
        {
            var Response = await _client.GetAsync($"/Catalog/{id}");
            return await Response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string Category)
        {
            var Response = await _client.GetAsync($"/catalog/category/{Category}");
            return await Response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
