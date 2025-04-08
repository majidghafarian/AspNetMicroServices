using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Commands.CheckOutOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddMediatR(typeof(UpdateOrderCommandHandler).Assembly);
            //services.AddMediatR(typeof(CheckOutOrderCommandHandler).Assembly);
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
