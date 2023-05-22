using Jhipster.Domain.Repositories.Interfaces;
using Jhipster.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Infrastructure.Data.Repositories
{
    public class JwtRepository: IJwtRepository
    {
        private readonly ApplicationDatabaseContext _applicationDatabaseContext;
        public JwtRepository(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _applicationDatabaseContext = applicationDatabaseContext;
        }

        public async Task<string> GetIdUser(string username)
        {
          var check =  await _applicationDatabaseContext.Users.FirstOrDefaultAsync(i=>i.UserName == username);
            return check.Id.ToString();
        }
        public async Task<string> CheckRegister(string Id)
        {
            Guid result;
            bool isValidGuid = Guid.TryParse(Id, out result);
            if (isValidGuid)
            {
                var idMerchant = new Guid(Id);
                var check = await _applicationDatabaseContext.Merchants.SingleOrDefaultAsync(i => i.Id == idMerchant);
                if (check == null)
                {
                    return "No exit Merchant";
                }
                else
                {
                    if (check.Status == 2 && check.AddressStatus == 2) return "true";
                    else return "false";
                }
            }
            else
            {
                return "Employee";
            }
        }
    }
}
