﻿using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Commands.WarehouseCm
{
    public class WarehouseProductAddCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Lot { get; set; }
        public DateTime DateExpire { get; set; }
        public int AvailabelQuantity { get; set; }
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
