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
    public class WarehouseUpdateCommand : IRequest<int>
    {

        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string WarehouseName { get; set; }
        public string? Descripton { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
    public class BrandUpdateCommandHandler : IRequestHandler<WarehouseUpdateCommand, int>
    {
        private readonly IWarehouseRepository _repo;
        private readonly IMapper _mapper;
        public BrandUpdateCommandHandler(IWarehouseRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(WarehouseUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Warehouse>(request);
            return await _repo.Update(obj);
        }
    }

}
