using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryApp.src.services.OrderService.OrderService.Domain.Entites;

namespace DeliveryApp.src.services.OrderService.OrderService.Infra.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(r => r.OrderId);
            builder.Property(r => r.OrderId).IsRequired().HasMaxLength(100);
        }
    }
}
