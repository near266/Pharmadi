using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Queries.CategoryQ
{
    public class CategoryGetAllAdminQuery : IRequest<PagedList<Category>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class CategoryGetAllAdminQueryHandler : IRequestHandler<CategoryGetAllAdminQuery, PagedList<Category>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryGetAllAdminQueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Category>> Handle(CategoryGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            // var obj = _mapper.Map<Category>(request);
            return await _repo.GetAllAdmin(request.page,request.pageSize);
        }
    }

}
