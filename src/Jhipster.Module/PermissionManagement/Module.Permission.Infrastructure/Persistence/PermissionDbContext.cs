using Jhipster.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Module.Permission.Core.Abstractions;
using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Infrastructure.Persistence
{
    public class PermissionDbContext : ModuleDbContext, IPermissionDbContext
    {

        public PermissionDbContext(DbContextOptions<PermissionDbContext> options) : base(options)
        {

        }

        public DbSet<Function> Functions { get; set; }
        public DbSet<FunctionType> FunctionTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleFunction> RoleFunctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
