using AutoMapper;
using MediatR;
using Module.Email.Domain.Entities;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Module.Factor.Application.Commands.UtmMechantCm
{
    public class AddUtmUserCommand :IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid? UtmId { get; set; }
        public string? UserId { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]


        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }
    }
    public class AddUtmUserCommandHandler : IRequestHandler<AddUtmUserCommand, int>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public AddUtmUserCommandHandler(IMerchantRepository MerchantRepository, IMapper mapper)
        {
            _repo = MerchantRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddUtmUserCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<UtmUser>(request);
            return await _repo.AddUtmUser(result);
        }
    }
}
