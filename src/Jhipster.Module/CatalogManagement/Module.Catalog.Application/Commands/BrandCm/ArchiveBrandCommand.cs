using AutoMapper;
using MediatR;
using Module.Catalog.Application.Commands.ProductCm;
using Module.Catalog.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.BrandCm
{
    public class ArchiveBrandCommand : IRequest<int>
    {
        public Guid? Id { get; set; }   
    }
    public class ArchiveBrandCommandHandler : IRequestHandler<ArchiveBrandCommand, int>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public ArchiveBrandCommandHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(ArchiveBrandCommand request, CancellationToken cancellationToken)
        {
            return await _repo.ArchiveBrand(request.Id);
        }
    }
}
