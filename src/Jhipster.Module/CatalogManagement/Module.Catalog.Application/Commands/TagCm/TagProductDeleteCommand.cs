using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.TagCm
{
    public class TagProductDeleteCommand : IRequest<int>
    {
        public Guid productId { get; set; }
    }
    public class TagProductDeleteCommandHandler : IRequestHandler<TagProductDeleteCommand, int>
    {
        private readonly ITagProductRepository _repo;
        private readonly IMapper _mapper;
        public TagProductDeleteCommandHandler(ITagProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(TagProductDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.productId);
        }
    }

}
