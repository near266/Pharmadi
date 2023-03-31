using Jhipster.Domain;
using System;
using System.Collections.Generic;

namespace Jhipster.DTO
{
    public class UpdateRoleDTO
    {
        public string user { get; set; }
        public IEnumerable<string> roles { get; set; }
    }
}
