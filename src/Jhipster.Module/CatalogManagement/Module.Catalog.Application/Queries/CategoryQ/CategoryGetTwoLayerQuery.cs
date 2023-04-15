using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.CategoryQ
{
    public class CategoryGetTwoLayerQuery : IRequest<IEnumerable<Category>>
    {
    }
    public class CategoryGetTwoLayerQueryHandler : IRequestHandler<CategoryGetTwoLayerQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryGetTwoLayerQueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Category>> Handle(CategoryGetTwoLayerQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetTwoLayer();
        }
    }

}
