using Microsoft.EntityFrameworkCore;
using Module.Factor.Domain.Entities;

namespace Module.Factor.Domain.Abstractions
{
    public interface IFactorDbContext
    {
        public DbSet<Merchant> Merchants { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
