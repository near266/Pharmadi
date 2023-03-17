using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Module.Permission.Core.Entities
{
    [Table("Roles")]
    public class Role : IdentityRole<string>
    {
        //public ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleFunction> RoleFunctions { get; set; }
    }
}
