using Jhipster.Domain;
using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Module.Factor.Domain.Abstractions;
using Module.Factor.Domain.Entities;

namespace Module.Factor.Infrastructure.Persistences
{
    public class FactorDbContext: ModuleDbContext, IFactorDbContext
    {
        public DbSet<Merchant> Merchants { get; set; }
       
        
        public FactorDbContext(DbContextOptions<FactorDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
