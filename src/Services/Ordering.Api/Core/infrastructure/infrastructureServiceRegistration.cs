using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.infrastructure.Repositories;
using Ordering.Application.Models;
using Ordering.Application.Contracts.infrastructure;
using Ordering.infrastructure.Mail;

namespace Ordering.infrastructure
{
    public static class infrastructureServiceRegistration
    {
        public static IServiceCollection AddinfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.Configure<EmailSettings>(x => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
