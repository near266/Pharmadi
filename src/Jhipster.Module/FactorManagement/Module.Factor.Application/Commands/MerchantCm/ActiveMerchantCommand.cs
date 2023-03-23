using MediatR;
using Module.Factor.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Factor.Application.Commands.MerchantCm
{
    public class ActiveMerchantCommand : IRequest<int>
    {
        public Guid id { get; set; }
    }
    public class ActiveMerchantCommandHandler : IRequestHandler<ActiveMerchantCommand, int>
    {
        private readonly IMerchantRepository _merchantRepository;
        public ActiveMerchantCommandHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public async Task<int> Handle(ActiveMerchantCommand request, CancellationToken cancellationToken)
        {
            await _merchantRepository.UpdateActiveMerchant(request.id);
            return 1;
        }
    }
}
