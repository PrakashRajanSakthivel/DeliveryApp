using DeliveryApp.src.services.OrderService.OrderService.Application.Model;
using DeliveryApp.src.services.OrderService.OrderService.Domain.Entites;
using DeliveryApp.src.services.OrderService.OrderService.Domain.Interfaces;

namespace DeliveryApp.src.services.OrderService.OrderService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderRequest request)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = request.UserId,
                RestaurantId = request.RestaurantId,
                Status = OrderStatus.Created,
                CreatedAt = DateTime.UtcNow,
                OrderItems = request.Items.Select(item => new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.OrderId;
        }
    }

}
