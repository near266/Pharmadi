﻿using AutoMapper;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Module.Factor.Application.Commands.MerchantCm
{
    public class MerchantAddCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string MerchantName { get; set; }
        public string? TaxCode { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
        public string? ContactName { get; set; }
        public string? GPPNumber { get; set; }
        public string? ContractNumber { get; set; }
        public int? Channel { get; set; }
        public int? Rank { get; set; }
        public string? Branch { get; set; }
        public string? TypeCustomer { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }

        public string? SubDistrict { get; set; }
        public DateTime? LicenseDate { get; set; }
        public string? LicensePlace { get; set; }

        public List<string>? GPPImage { get; set; }

        public string? Avatar { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public int? Status { get; set; }

        public string? Email { get; set; }

        public int? AddressStatus { get; set; }
    }
    public class MerchantAddCommandHandler: IRequestHandler<MerchantAddCommand, int>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public MerchantAddCommandHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(MerchantAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Merchant>(request);
            return await _repo.Add(obj);
        }
    }

}
