using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Queries.CategoryQ
{
    public class CategorySearchQuery : IRequest<IEnumerable<Category>>
    {
        public string? keyword { get; set; }
    }
    public class CategorySearchQueryHandler : IRequestHandler<CategorySearchQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategorySearchQueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Category>> Handle(CategorySearchQuery request, CancellationToken cancellationToken)
        {
            return await _repo.Search(request.keyword);
        }
    }

}
