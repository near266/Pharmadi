using Jhipster.Domain;
using System.Collections.Generic;

namespace Jhipster.DTO
{
    public class UpdateRoleDTO
    {
        public User user { get; set; }
        public IEnumerable<string> roles { get; set; }
    }
}
