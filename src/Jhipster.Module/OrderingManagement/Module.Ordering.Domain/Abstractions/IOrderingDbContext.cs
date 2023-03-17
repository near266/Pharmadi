using Microsoft.EntityFrameworkCore;
using Module.Ordering.Domain.Entities;

namespace Module.Ordering.Domain.Abstractions
{
    public interface IOrderingDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
