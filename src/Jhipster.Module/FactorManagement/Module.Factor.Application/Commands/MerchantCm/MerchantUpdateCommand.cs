﻿using AutoMapper;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Factor.Application.Commands.MerchantCm
{
    public class MerchantUpdateCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string MerchantName { get; set; }
        public string? TaxCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
        public string? ContractNumber { get; set; }
        public int? Channel { get; set; }
        public int? Rank { get; set; }
        public string? Branch { get; set; }
        public string? TypeCustomer { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
    public class MerchantUpdateCommandHandler: IRequestHandler<MerchantUpdateCommand, int>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public MerchantUpdateCommandHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(MerchantUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Merchant>(request);
            return await _repo.Update(obj);
        }
    }

}
