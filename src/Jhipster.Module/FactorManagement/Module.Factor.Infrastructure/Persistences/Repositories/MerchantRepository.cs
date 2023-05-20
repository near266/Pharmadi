using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using Module.Factor.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Module.Factor.Application.DTO;
using Jhipster.Infrastructure.Data;
using Module.Email.Domain.Entities;

namespace Module.Factor.Infrastructure.Persistence.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly FactorDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _dbContext;
        public MerchantRepository(FactorDbContext context, IMapper mapper,ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = context;
            _mapper = mapper;
            _dbContext = applicationDatabaseContext;
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

        public async Task<PagedList<MerchantAdminDTO>> GetAllAdmin(int page, int pageSize, string? name, DateTime? StartDate, DateTime? EndDate, int? Status, string? Email, string? PhoneNumber)
        {
            var query = _context.Merchants.AsQueryable();
            if (name != null)
            {
                name = name.ToLower();
                query = query.Where(i => i.MerchantName.ToLower().Contains(name));
            }

            query = Status != null ? query.Where(i => i.Status == Status) : query;
            query = Email != null ? query.Where(i => i.Email.Contains(Email)) : query;
            query = PhoneNumber != null ? query.Where(i => i.PhoneNumber.Contains(PhoneNumber)) : query;
            query = StartDate != null ? query.Where(i => i.CreatedDate > StartDate) : query;
            query = EndDate != null ? query.Where(i => i.CreatedDate < EndDate) : query;
            var dataUser = _dbContext.Users.AsQueryable();
            var dataMerchant = new List<MerchantAdminDTO>();
            foreach (var item1 in query)
                foreach (var item2 in dataUser)
                {
                    if (item1.Id.ToString() == item2.Id)
                    {
                        dataMerchant.Add(new MerchantAdminDTO()
                        {
                            Id = item1.Id,
                            TaxCode = item1.TaxCode,
                            MerchantName = item1.MerchantName,
                            PhoneNumber = item1.PhoneNumber,
                            Address = item1.Address,
                            Location = item1.Location,
                            ContactName = item1.ContactName,
                            GPPNumber = item1.GPPNumber,
                            ContractNumber = item1.ContractNumber,
                            Channel = item1.Channel,
                            Rank = item1.Rank,
                            Branch = item1.Branch,
                            TypeCustomer = item1.TypeCustomer,
                            Status = item1.Status,
                            Email = item1.Email,
                            City = item1.City,
                            District = item1.District,
                            SubDistrict = item1.SubDistrict,
                            LicenseDate = item1.LicenseDate,
                            LicensePlace = item1.LicensePlace,
                            GPPImage = item1.GPPImage,
                            Avatar = item1.Avatar,
                            AddressStatus = item1.AddressStatus,
                            CreatedBy = item1.CreatedBy,
                            CreatedDate = item1.CreatedDate,
                            Login = item2.Login,
                            LastModifiedBy = item1.LastModifiedBy,
                            LastModifiedDate = item1.LastModifiedDate!=null ? item1.LastModifiedDate:item1.CreatedDate,
                        });
                    }
                }

            var data = dataMerchant.OrderByDescending(i=>i.LastModifiedDate)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize);
            var res = new PagedList<MerchantAdminDTO>();
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
            data.AddressStatus = 2;
            await _context.SaveChangesAsync();
        }
        public async Task<Merchant> ViewDetail(Guid id)
        {
            var res = await _context.Merchants.Where(i => i.Id == id).FirstOrDefaultAsync();
            return res;
        }

        public async Task<IEnumerable<Merchant>> SearchToChoose(string? keyword)
        {
            var query = _context.Merchants.Where(i => i.Status == 2 && i.AddressStatus == 2).AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.MerchantName.ToLower().Contains(keyword) || (i.Address != null ? i.Address : "null").ToLower().Contains(keyword)
                                || i.PhoneNumber.Contains(keyword));
            }
            var data = query.AsEnumerable();
            return data;
        }

        public async Task<int> UpdateAddressStatus(Guid userid)
        {
            var old = await _context.Merchants.FirstOrDefaultAsync(i => i.Id.Equals(userid));
            if (old != null)
            {
                old.Status = 2;
                old.AddressStatus = 2;
                return await _context.SaveChangesAsync(default);
            }
            return -1;
        }

        public async Task<int> AddUtmMerchant(Utm utm)
        {
            _dbContext.Utms.Add(utm);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
