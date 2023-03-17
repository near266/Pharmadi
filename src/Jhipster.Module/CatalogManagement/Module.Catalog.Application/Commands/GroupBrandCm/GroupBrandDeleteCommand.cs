using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.GroupBrandCm
{
    public class GroupBrandDeleteCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class GroupBrandDeleteCommandHandler : IRequestHandler<GroupBrandDeleteCommand, int>
    {
        private readonly IGroupBrandRepository _repo;
        private readonly IMapper _mapper;
        public GroupBrandDeleteCommandHandler(IGroupBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(GroupBrandDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
