using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.WarehouseCm
{
    public class WarehouseProductDeleteCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class WarehouseProductDeleteCommandHandler : IRequestHandler<WarehouseProductDeleteCommand, int>
    {
        private readonly IWarehouseProductRepository _repo;
        private readonly IMapper _mapper;
        public WarehouseProductDeleteCommandHandler(IWarehouseProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(WarehouseProductDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
