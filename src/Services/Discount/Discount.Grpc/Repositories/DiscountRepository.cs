using Dapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Repostories;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;

        public DiscountRepository(string connectionString)
        {
            _connectionString =connectionString;

            // برای تست
            Console.WriteLine($"Connection String: {_connectionString}");
        }
 

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                var query = await connection.ExecuteAsync(
                    "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { productname = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
                );
                return query > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteDiscount(string ProductName)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                var query = await connection.ExecuteAsync(
                    "DELETE FROM Coupon WHERE ProductName = @ProductName",
                    new { ProductName }
                );
                return query > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Coupon> GetDiscount(string ProductName)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                    "SELECT * FROM Coupon WHERE ProductName = @ProductName",
                    new { ProductName }
                );
                return coupon ?? new Coupon { ProductName = "no discount", Amount = 0, Description = "no discount desc" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Coupon { ProductName = "error", Amount = 0, Description = "error fetching discount" };
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                var query = await connection.ExecuteAsync(
                    "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                    new { productname = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id }
                );
                return query > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

}
