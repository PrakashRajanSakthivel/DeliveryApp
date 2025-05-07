using DeliveryApp.src.services.OrderService.OrderService.Domain.Entites;

namespace DeliveryApp.src.services.OrderService.OrderService.Infra.Repository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid orderId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task SaveChangesAsync();
    }

}
