using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Email.Application.Persistences;
using Module.Email.Domain.Entities;
using Module.Email.Infrastructure.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Infrastructure.Extensions.Persistences.Repositories
{
    public class UtmRepository : IUtmRepository
    {
        private readonly EmailDbContext _context;
        private readonly IMapper _mapper;
        public UtmRepository(EmailDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Utm utm)
        {
             _context.Utms.Add(utm);
            return await _context.SaveChangesAsync();
        }
    }
}
