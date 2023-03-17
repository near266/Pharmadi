using Microsoft.EntityFrameworkCore;
using Module.Permission.Core.Entities;

namespace Module.Permission.Core.Abstractions
{
    public interface IPermissionDbContext
    {
        public DbSet<Function> Functions { get; set; }
        public DbSet<FunctionType> FunctionTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleFunction> RoleFunctions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
