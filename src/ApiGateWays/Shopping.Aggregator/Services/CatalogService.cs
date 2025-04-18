using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _client.GetAsync("api/v1/catalog/GetAllProducts");
            return await response.ReadContentAs<List<CatalogModel>>();
        }


        public async Task<CatalogModel> GetCatalog(Guid id)
        {
            var response = await _client.GetAsync($"api/v1/catalog/GetByIdproduct?id={id}");

            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string categoryName)
        {
            var response = await _client.GetAsync($"api/v1/catalog/GetProductByCategory/{categoryName}");

            return await response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
