using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Module.Factor.Application.Persistences;
using Module.Email.Domain.Entities;

namespace Module.Factor.Application.Commands.MerchantCm
{
    public class AddUtmMerchantCommand : IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Utmlink { get; set; }
        public string? Campaign { get; set; }
        public string? Content { get; set; }
        public string? Medium { get; set; }
        public string? Source { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateRegister { get; set; }
        [JsonIgnore]

        public string? CreatedBy { get; set; }
        [JsonIgnore]

        public DateTime CreatedDate { get; set; }
        [JsonIgnore]


        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }
    }
    public class AddUtmMerchantCommandHandler : IRequestHandler<AddUtmMerchantCommand, int>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public AddUtmMerchantCommandHandler(IMerchantRepository MerchantRepository, IMapper mapper)
        {
            _repo = MerchantRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddUtmMerchantCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Utm>(request);
            return await _repo.AddUtmMerchant(result);
        }
    }
}
