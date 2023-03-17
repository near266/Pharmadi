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
    public class WarehouseProductAddCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid WarehouseId { get; set; }
        public int AvaiableQuantity { get; set; }
    }
    public class WarehouseProductAddCommandHandler : IRequestHandler<WarehouseProductAddCommand, int>
    {
        private readonly IWarehouseProductRepository _repo;
        private readonly IMapper _mapper;
        public WarehouseProductAddCommandHandler(IWarehouseProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(WarehouseProductAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WarehouseProduct>(request);
            return await _repo.Add(obj);
        }
    }

}
