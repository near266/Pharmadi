using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Domain.Repositories.Interfaces
{
    public interface  IJwtRepository
    {
        Task<string> GetIdUser(string username);
    }
}
