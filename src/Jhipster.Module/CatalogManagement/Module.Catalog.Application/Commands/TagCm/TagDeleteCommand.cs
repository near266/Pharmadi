using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.TagCm
{
    public class TagDeleteCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class TagDeleteCommandHandler : IRequestHandler<TagDeleteCommand, int>
    {
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;
        public TagDeleteCommandHandler(ITagRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(TagDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
