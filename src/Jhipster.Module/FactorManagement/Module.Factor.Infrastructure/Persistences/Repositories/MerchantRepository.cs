using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using Module.Factor.Infrastructure.Persistences;
using Jhipster.Service.Utilities;

namespace Module.Factor.Infrastructure.Persistence.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly FactorDbContext _context;
        private readonly IMapper _mapper;
        public MerchantRepository(FactorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Merchant request)
        {
            await _context.Merchants.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Merchants.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Merchants.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<PagedList<Merchant>> GetAllAdmin(int page, int pageSize, string? name, DateTime? StartDate, DateTime? EndDate, int? Status)
        {
            var query = _context.Merchants.AsQueryable();
            if (name != null)
            {
                name = name.ToLower();
                query = query.Where(i => i.MerchantName.ToLower().Contains(name));
            }
           
            query = Status != null ? query.Where(i => i.Status == Status) : query;
            query = StartDate != null ? query.Where(i => i.CreatedDate > StartDate) : query;
            query=EndDate!=null ?query.Where(i=>i.CreatedDate < EndDate) : query;   
            var data = query
                        .Skip(pageSize * (page-1))
                        .Take(pageSize);
            var res = new PagedList<Merchant>();
            res.Data = data;
            res.TotalCount = query.Count();
            return res;
        }

        public async Task<int> Update(Merchant request)
        {
            var old = await _context.Merchants.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Merchant, Merchant>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
        public async Task UpdateActiveMerchant(Guid id)
        {
            var data = await _context.Merchants.FirstOrDefaultAsync(i => i.Id == id);
            data.Status = 1;
            await _context.SaveChangesAsync();
        }
        public async Task<Merchant> ViewDetail(Guid id)
        {
            var res = await _context.Merchants.Where(i => i.Id == id).FirstOrDefaultAsync();
            return res;
        }

        public async Task<IEnumerable<Merchant>> SearchToChoose(string? keyword)
        {
            var query = _context.Merchants.AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.MerchantName.ToLower().Contains(keyword) || (i.Address != null ? i.Address : "null").ToLower().Contains(keyword)
                                || i.PhoneNumber.Contains(keyword));
            }
            var data = query.AsEnumerable();
            return data;
        }
    }
}
