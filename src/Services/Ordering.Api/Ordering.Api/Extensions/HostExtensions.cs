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
                    logger.LogError(ex, "An error occurred while migrating the database");
                    if (retryforavaliblity < 50)
                    {
                        retryforavaliblity++;
                        MigrationDataBase<TContext>(host, seeder, retryforavaliblity);
                    }
                }
            }
            return host;
        }

        public static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
            TContext context, IServiceProvider service) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, service);
        }
    }
}
