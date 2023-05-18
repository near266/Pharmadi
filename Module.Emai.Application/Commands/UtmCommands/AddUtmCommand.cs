using AutoMapper;
using MediatR;
using Module.Email.Application.Persistences;
using Module.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Application.Commands
{
    public class AddUtmCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? Utmlink { get; set; }
        public string? Campaign { get; set; }
        public string? Content { get; set; }
        public string? Medium { get; set; }
        public string? Source { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateRegister { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
    public class AddUtmCommandHandler : IRequestHandler<AddUtmCommand, int>
    {
        private readonly IUtmRepository _repo;
        private readonly IMapper _mapper;
      public AddUtmCommandHandler (IUtmRepository utmRepository, IMapper mapper)
        {
            _repo = utmRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddUtmCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Utm>(request);
            return await _repo.Add(result);
        }
    }
}
