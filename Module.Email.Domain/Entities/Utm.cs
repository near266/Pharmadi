using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Domain.Entities
{
    public class Utm : BaseEntity<Guid>
    {
        public string? Utmlink { get; set; }
        public string? Campaign { get; set; }
        public string? Content { get; set; }
        public string? Medium { get; set; }
        public string? Source { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateRegister { get; set; }
     
    }
}
