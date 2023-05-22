using Jhipster.Domain;
using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Module.Email.Domain.Abstractions;
using Module.Email.Domain.Entities;
using Module.Factor.Domain.Abstractions;
using Module.Factor.Domain.Entities;

namespace Module.Email.Infrastructure.Persistences
{
    public class EmailDbContext: ModuleDbContext, IEmailDbContext
    {
       public DbSet<Module.Email.Domain.Entities.Email> emails { get; set; }
       public DbSet<Utm> Utms { get; set; }
       public DbSet<UtmUser> UtmUsers { get; set; }
       
        
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
     
        }
    }
}
