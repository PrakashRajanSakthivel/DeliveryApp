using DeliveryApp.src.services.OrderService.OrderService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryApp.src.shared.Infra
{
   
        public static class ServiceCollectionExtensions
        {
            public static IServiceCollection AddOrderServiceInfrastructure(this IServiceCollection services)
            {
                services.AddScoped<IOrderRepository, OrderRepository>();
                // Add more services/repositories here later

                return services;
            }
        }
    }


