using DeliveryApp.src.services.OrderService.OrderService.Application.Model;

namespace DeliveryApp.src.services.OrderService.OrderService.Application.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderRequest request);
    }

}
