using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.GroupBrandCm
{
    public class PinGroupBrandCommand :IRequest<int>
    {
        public Guid Id { get; set; }
        public bool Pin { get; set; }   
    }
    public class PinGroupBrandCommandHandler : IRequestHandler<PinGroupBrandCommand, int>
    {
        private readonly IGroupBrandRepository _repo;
        private readonly IMapper _mapper;
        public PinGroupBrandCommandHandler(IGroupBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(PinGroupBrandCommand request, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<GroupBrand>(request);
            return await _repo.PinGroup(res.Id, res.Pin);
        }
    }
}
