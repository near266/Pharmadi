﻿using AutoMapper;
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

        public async Task<PagedList<Merchant>> GetAllAdmin(int page, int pageSize, string? keyword)
        {
            var query =  _context.Merchants.AsQueryable();
            if(keyword!=null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i=>i.MerchantName.ToLower().Contains(keyword));
            }
            var data = query
                        .Skip(pageSize * page)
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

        public async Task<Merchant> ViewDetail(Guid id)
        {
            var res = await _context.Merchants.Where(i => i.Id == id).FirstOrDefaultAsync();
            return res;
        }
    }
}