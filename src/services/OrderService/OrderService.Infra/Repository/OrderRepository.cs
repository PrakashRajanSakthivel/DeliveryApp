using DeliveryApp.src.services.OrderService.OrderService.Domain.Entites;
using DeliveryApp.src.services.OrderService.OrderService.Domain.Interfaces;
using DeliveryApp.src.services.OrderService.OrderService.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.src.services.OrderService.OrderService.Infra.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.StatusHistories)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task DeleteAsync(Order order)
        {
             _context.Orders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
