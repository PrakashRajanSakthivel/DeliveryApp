namespace DeliveryApp.src.services.OrderService.OrderService.Application.Common
{
    public interface IRestaurentService
    {
        Task<bool> ProcessPaymentAsync(string orderId, decimal amount);
    }
}
