using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Jhipster.Domain
{
    public class Role : IdentityRole<string>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
