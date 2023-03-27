using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.BrandCm
{
    public class AddListBrandCommand :IRequest<int>
    {
        public List<AddBrandDTO> Brands { get; set; }
    }
    public class AddListBrandCommandHandler : IRequestHandler<AddListBrandCommand, int>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public AddListBrandCommandHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(AddListBrandCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<List<Brand>>(request);
            return await _repo.AddListBrand(obj);
        }
    }
}
