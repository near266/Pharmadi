using Module.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Application.Persistences
{
    public interface IUtmRepository
    {
        Task<int> Add(Utm utm);
        Task<int> Delete(Guid id);
        Task<int> Update(Utm utm);
        Task<IEnumerable<Utm>> GetAll();
        Task<Utm> GetDetail(Guid id);
    }
}
