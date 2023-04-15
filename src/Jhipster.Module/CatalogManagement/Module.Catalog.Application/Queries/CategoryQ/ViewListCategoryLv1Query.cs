using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.CategoryQ
{
    public class ViewListCategoryLv1Query : IRequest<IEnumerable<Category>>
    {
    }
    public class ViewListCategoryLv1QueryHandler : IRequestHandler<ViewListCategoryLv1Query, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public ViewListCategoryLv1QueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Category>> Handle(ViewListCategoryLv1Query request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllCategoriesLv1();
        }
    }
}
