using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.ProductCm
{
    public class UpdateStatusProductCommand :IRequest<int>
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
    public class UpdateStatusProductCommandHandler : IRequestHandler<UpdateStatusProductCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public UpdateStatusProductCommandHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(UpdateStatusProductCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Product>(request);
            return await _repo.UpdataStatusProduct(obj.Id,obj.Status);
        }
    }
}
