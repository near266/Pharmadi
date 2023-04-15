using Microsoft.EntityFrameworkCore;
using Module.Catalog.Domain.Entities;
//using Module.Ordering.Domain.Entities;

namespace Module.Catalog.Domain.Abstractions
{
    public interface ICatalogDbContext
    {
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
        public DbSet<PostContent> PostContents { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
