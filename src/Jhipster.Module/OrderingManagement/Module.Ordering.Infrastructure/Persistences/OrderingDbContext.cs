using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Domain.Entities;
using Module.Factor.Domain.Entities;
using Module.Ordering.Domain.Abstractions;
using Module.Ordering.Domain.Entities;

namespace Module.Ordering.Infrastructure.Persistences
{
    public class OrderingDbContext: ModuleDbContext, IOrderingDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<GroupBrand> GroupBrands { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<TagProduct> TagProducts { get; set; }
        public DbSet<LabelProduct> LabelProducts { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public DbSet<HistoryOrder> HistoryOrders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductDiscount> productDiscounts { get; set; }

        public DbSet<ProductSale> ProductSales { get; set; }
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
