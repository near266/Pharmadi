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

namespace Module.Catalog.Application.Commands.GroupBrandCm
{
    public class GroupBrandAddCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string GroupBrandName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class GroupBrandAddCommandHandler : IRequestHandler<GroupBrandAddCommand, int>
    {
        private readonly IGroupBrandRepository _repo;
        private readonly IMapper _mapper;
        public GroupBrandAddCommandHandler(IGroupBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(GroupBrandAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<GroupBrand>(request);
            return await _repo.Add(obj);
        }
    }

}
