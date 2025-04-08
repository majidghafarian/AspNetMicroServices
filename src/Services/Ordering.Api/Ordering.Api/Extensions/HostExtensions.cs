using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrationDataBase<TContext>(this IHost host,
            Action<TContext, IServiceProvider> seeder, int? retry = 0)
            where TContext : DbContext
        {
            int retryforavaliblity = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                    InvokeSeeder(seeder, context, services);
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database. Retry attempt {Retry}", retryforavaliblity);

                    if (retryforavaliblity < 5)
                    {
                        retryforavaliblity++;
                        Thread.Sleep(2000); // یا await Task.Delay(2000); اگر متد async باشه
                        MigrationDataBase<TContext>(host, seeder, retryforavaliblity);
                    }
                    else
                    {
                        logger.LogError("Migration failed after {Retry} attempts", retryforavaliblity);
                        throw; // اگه بخوای بعد از چند بار تلاش بی‌نتیجه اپلیکیشن fail بشه
                    }
                }

            }
            return host;
        }

        public static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
          TContext context, IServiceProvider service) where TContext : DbContext
        {
            try
            {
                context.Database.Migrate(); // اعمال مایگریشن‌ها
                seeder(context, service);   // اجرای Seeder
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error during database migration or seeding:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message); // اگه InnerException هم داشت
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
                throw; // اگه خواستی جلوش رو نگیره و همون خطا بالا بره
            }
        }

    }
}
