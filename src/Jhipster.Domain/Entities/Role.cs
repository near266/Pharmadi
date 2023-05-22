using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Jhipster.Domain
{
    public class Role : IdentityRole<string>
    {
        [NotMapped]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
