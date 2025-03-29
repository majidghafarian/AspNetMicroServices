using Dapper;
using Discount.api.Entities;
using Discount.api.Repostories;
using Npgsql;

namespace Discount.api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var Conection = new NpgsqlConnection
            (_configuration.GetValue<string>("databasesettings:connectionstring"));
            var query =
               await Conection.ExecuteAsync
               ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)  ",
                        new { productname = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (query==0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string ProductName)
        {
            using var Conection = new NpgsqlConnection
      (_configuration.GetValue<string>("databasesettings:connectionstring"));
            var query =
               await Conection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName =@ProductName",
               new { ProductName = ProductName });

            if (query==0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetDiscount(string ProductName)
        {
            using var Conection = new NpgsqlConnection
                (_configuration.GetValue<string>("databasesettings:connectionstring"));
            var Coupon = await Conection.QueryFirstOrDefaultAsync<Coupon>(
              "SELECT * FROM Coupon WHERE ProductName = @ProductName",
              new { ProductName = ProductName }
          );

            if (Coupon==null)
            {
                return new Coupon { ProductName="no discount", Amount=0, Description="no discount desc" };
            }
            return Coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var Conection = new NpgsqlConnection
     (_configuration.GetValue<string>("databasesettings:connectionstring"));
            var query =
               await Conection.ExecuteAsync
               ("UPDATE  Coupon SET  ProductName=@ProductName, Description= @Description, Amount=@Amount WHERE Id=@Id)  ",
                        new { productname = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (query==0)
            {
                return false;
            }
            return true;
        }
    }
}
