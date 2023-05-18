using Microsoft.EntityFrameworkCore;
using Module.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Domain.Abstractions
{
    public interface IEmailDbContext
    {
       public DbSet<Module.Email.Domain.Entities.Email> emails { get; set; }
       public DbSet<Utm> Utms { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
