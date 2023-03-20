using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Domain.Abstractions;
using Module.Ordering.Domain.Entities;

namespace Module.Ordering.Infrastructure.Persistences
{
    public class OrderingDbContext: ModuleDbContext, IOrderingDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
