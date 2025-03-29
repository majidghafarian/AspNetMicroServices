using Catalog.Api.Entities;

namespace Catalog.Api.Repostories
{
    public interface IProductRepository
    {
        // متد برای افزودن یک محصول جدید
        Task AddProductAsync(Product product);

        // متد برای دریافت همه محصولات
        Task<IEnumerable<Product>> GetAllProductsAsync();

        // متد برای دریافت محصول بر اساس شناسه (ID)
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable< Product>> GetProductByNameAsync(string Name);
        Task<Product> GetProductByCategoryAsync(string CategoryName);

        // متد برای به‌روزرسانی محصول موجود
        Task <bool>UpdateProductAsync(  Product updatedProduct);

        // متد برای حذف محصول
        Task<bool> DeleteProductAsync(Guid id);
    }
}
