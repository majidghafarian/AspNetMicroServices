using Npgsql;

namespace Discount.Grpc.Extension
{
    public static class MigrationManager
    {
        public static IHost MigrationDatabase<T>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var Configuration = services.GetRequiredService<IConfiguration>();
                var Logger = services.GetRequiredService<ILogger<T>>();

                try
                {
                    // اینجا می‌توان عملیات اتصال به دیتابیس را انجام داد
                    Console.WriteLine("Database migration checked.");
                    
                    using var Conection = new NpgsqlConnection(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
                
                    Conection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection= Conection
                    };
                    command.CommandText="DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText=@"CREATE TABLE Coupon(
                                                   ID SERIAL PRIMARY KEY  NOT NULL,
                                                   ProductName VARCHAR(24) NOT NULL,
                                                   Description TEXT,
                                                   Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText=@"INSERT INTO Coupon (ProductName, Description, Amount) 
                                                 VALUES 
                                                       ('Iphon11', 'Discount for iphon11', 120), 
                                                       ('Samsung', 'Discount for samsun', 10);";
                    command.ExecuteNonQuery();
                    Logger.LogInformation("Migrated Postgresql database");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }
            return host;
        }
    }

}
