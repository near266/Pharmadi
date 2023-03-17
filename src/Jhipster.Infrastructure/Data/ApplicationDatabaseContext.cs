using Jhipster.Domain;
using Jhipster.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Domain.Entities;
using Module.Catalog.Domain.Entities;

namespace Jhipster.Infrastructure.Data
{
    public class ApplicationDatabaseContext : IdentityDbContext<
        User, Role, string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>
    >
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region 1. Catalog module
        public DbSet<GroupBrand> GroupBrands { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagProduct> TagProducts { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<LabelProduct> LabelProducts { get; set; }
        public DbSet<PostContent> PostContents { get; set; }

        public DbSet<Module.Catalog.Domain.Entities.Product> Products { get; set; }

        #endregion

        #region 2. Ordering module

        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ordering> Orderings { get; set; }
        #endregion

        #region 3. Factor module
        public DbSet<Module.Factor.Domain.Entities.Merchant> Merchants { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>().Ignore(c => c.User);
            // Rename AspNet default tables
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Ordering>()
               .HasMany(e => e.OrderItems)
               .WithOne()
               .HasForeignKey(e => e.OrderingId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            #region 1.Config Product

            builder.Entity<Module.Catalog.Domain.Entities.Product>(u =>
            {
                u.ToTable("Products");
                u.Property(c => c.ProductName).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.ProductName));
                u.Property(c => c.SKU).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.SKU));
                u.Property(c => c.Function).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.Function));
                u.Property(c => c.ListPrice).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.ListPrice));
                u.Property(c => c.SalePrice).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.SalePrice));
                u.Property(c => c.UnitName).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.UnitName));
                u.Property(c => c.Description).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.Description));
                
            });
            builder.Entity<Module.Ordering.Domain.Entities.Product>(u =>
            {
                u.ToTable("Products");
                u.Property(c => c.ProductName).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.ProductName));
                u.Property(c => c.SKU).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.SKU));
                u.Property(c => c.Function).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.Function));
                u.Property(c => c.ListPrice).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.ListPrice));
                u.Property(c => c.SalePrice).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.SalePrice));
                u.Property(c => c.UnitName).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.UnitName));
                u.Property(c => c.Description).HasColumnName(nameof(Module.Catalog.Domain.Entities.Product.Description));
                u.HasOne<Module.Catalog.Domain.Entities.Product>().WithOne().HasForeignKey<Module.Catalog.Domain.Entities.Product>(e => e.Id);
            });

            #endregion

            #region 2. Config Merchant
            builder.Entity<Module.Factor.Domain.Entities.Merchant>(u =>
            {
                u.ToTable("Merchants");
                u.Property(c => c.TaxCode).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.TaxCode));
                u.Property(c => c.MerchantName).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.MerchantName));
                u.Property(c => c.PhoneNumber).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.PhoneNumber));
                u.Property(c => c.Address).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.Address));
                u.Property(c => c.Location).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.Location));
                u.Property(c => c.ContactName).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.ContactName));
                u.Property(c => c.GPPNumber).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.GPPNumber));
            });
            builder.Entity<Module.Ordering.Domain.Entities.Merchant>(u =>
            {
                u.ToTable("Merchants");
                u.Property(c => c.TaxCode).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.TaxCode));
                u.Property(c => c.MerchantName).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.MerchantName));
                u.Property(c => c.PhoneNumber).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.PhoneNumber));
                u.Property(c => c.Address).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.Address));
                u.Property(c => c.Location).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.Location));
                u.Property(c => c.ContactName).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.ContactName));
                u.Property(c => c.GPPNumber).HasColumnName(nameof(Module.Factor.Domain.Entities.Merchant.GPPNumber));
                u.HasOne<Module.Factor.Domain.Entities.Merchant>().WithOne().HasForeignKey<Module.Factor.Domain.Entities.Merchant>(e => e.Id);
            });

            #endregion
        }

        /// <summary>
        /// SaveChangesAsync with entities audit
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is IAuditedEntityBase && (
                  e.State == EntityState.Added
                  || e.State == EntityState.Modified));

            string modifiedOrCreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditedEntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((IAuditedEntityBase)entityEntry.Entity).CreatedBy = modifiedOrCreatedBy;
                }
                else
                {
                    Entry((IAuditedEntityBase)entityEntry.Entity).Property(p => p.CreatedDate).IsModified = false;
                    Entry((IAuditedEntityBase)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
              ((IAuditedEntityBase)entityEntry.Entity).LastModifiedDate = DateTime.Now;
                ((IAuditedEntityBase)entityEntry.Entity).LastModifiedBy = modifiedOrCreatedBy;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
