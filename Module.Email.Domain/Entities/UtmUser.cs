using Jhipster.Domain;
using Module.Email.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Domain.Entities
{
    public class UtmUser : BaseEntity<Guid>
    {
        public Guid? UtmId { get; set; }
        public string? UserId { get; set; }
        public Utm? Utms { get; set; }
        public User? Users { get; set; }
    }
}
