using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.BrandCm
{
    public class IsBrandEmtyGroupCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }

        public class IsBrandEmtyGroupCommandHandler : IRequestHandler<IsBrandEmtyGroupCommand, bool>
        {
            private readonly IBrandRepository _repo;
            private readonly IMapper _mapper;
            public IsBrandEmtyGroupCommandHandler(IBrandRepository repo, IMapper mapper)
            {
                _repo = repo ?? throw new ArgumentNullException(nameof(repo));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public async Task<bool> Handle(IsBrandEmtyGroupCommand request, CancellationToken cancellationToken)
            {
                return await _repo.IsBrandEmtyGroup(request.Id);
            }
        }
    }
}