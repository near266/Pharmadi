

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.PostQ
{
    public class PostViewDetail : IRequest<PostContent>
    {
        public Guid Id { get; set; }
    }
    public class PostViewDetailHandler : IRequestHandler<PostViewDetail, PostContent>
    {
        private readonly IPostContentRepository _repo;
        private readonly IMapper _mapper;
        public PostViewDetailHandler(IPostContentRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PostContent> Handle(PostViewDetail request, CancellationToken cancellationToken)
        {
            return await _repo.ViewDetail(request.Id);
        }
    }

}
