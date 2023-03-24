using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.GroupBrandCm
{
    public class GroupBrandUpdateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Id { get; set; }
        public string GroupBrandName { get; set; }
        public string? LogoGroupBrand { get; set; }
        public bool? Pin { get; set; }
        [JsonIgnore]
        public Guid? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class GroupBrandUpdateCommandHandler : IRequestHandler<GroupBrandUpdateCommand, int>
    {
        private readonly IGroupBrandRepository _repo;
        private readonly IMapper _mapper;
        public GroupBrandUpdateCommandHandler(IGroupBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(GroupBrandUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<GroupBrand>(request);
            return await _repo.Update(obj);
        }
    }

}
