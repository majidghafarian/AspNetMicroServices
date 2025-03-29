using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repostories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }


        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);  // ایجاد فیلتر برای پیدا کردن محصول با شناسه موردنظر
            var result = await _context.Products.DeleteOneAsync(filter);  // استفاده از فیلتر برای حذف محصول
            return result.IsAcknowledged&& result.DeletedCount  > 0;  // بررسی اینکه آیا محصول حذف شده است یا خیر
        }




        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }


        public Task<Product> GetProductByCategoryAsync(string CategoryName)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string Name)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression(Name, "i"));  // "i" برای جستجوی غیرحساس به حروف
            return await _context.Products
                                  .Find(filter)
                                  .ToListAsync();
        }



        public async Task<bool> UpdateProductAsync( Product updatedProduct)
        {
            var result = await _context
                .Products
                .ReplaceOneAsync(p => p.Id == updatedProduct.Id, updatedProduct);
            return result
                .IsAcknowledged 
                && result.ModifiedCount > 0;
        }

    }
}
