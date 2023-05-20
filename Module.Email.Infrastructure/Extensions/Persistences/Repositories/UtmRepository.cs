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

        public async Task<int> Delete(Guid id)
        {
            var check = await _context.Utms.FirstOrDefaultAsync(x => x.Id == id);
            if(check != null)
            {
                 _context.Utms.Remove(check);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<Utm>> GetAll()
        {
            var result = await _context.Utms.ToListAsync();
            return result;
        }

        public async Task<Utm> GetDetail(Guid id)
        {
            var result = await _context.Utms.FirstOrDefaultAsync(i=> i.Id == id);
            return result;
        }

        public async Task<int> Update(Utm utm)
        {
            var old = await _context.Utms.FirstOrDefaultAsync(i=>i.Id ==utm.Id);
            if(old != null)
            {
                _mapper.Map<Utm,Utm>(utm,old);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
