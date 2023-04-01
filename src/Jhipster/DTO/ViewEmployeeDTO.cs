using System;

namespace Jhipster.DTO
{
    public class ViewEmployeeDTO
    {
        public string? Name { get; set; }
        //public DateTime? FromDate { get; set; }
        //public DateTime? ToDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? Email { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
