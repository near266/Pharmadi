using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.BrandCm
{
    public class BrandPinCommand :IRequest<int>
    {
        public Guid Id { get; set; }
        public bool Pin { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]

        public string? LastModifiedBy { get; set; }
      
      }
    public class BrandPinCommandHandler : IRequestHandler<BrandPinCommand, int>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandPinCommandHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(BrandPinCommand request, CancellationToken cancellationToken)
        {
            var req = _mapper.Map<Brand>(request);
            return await _repo.PinBrand(req);
        }
    }
}
