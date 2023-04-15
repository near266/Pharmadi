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
    public class ViewListCategoryLv2Query :IRequest<IEnumerable<Category>>
    {
    }
    public class ViewListCategoryLv2QueryHandler : IRequestHandler<ViewListCategoryLv2Query, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public ViewListCategoryLv2QueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Category>> Handle(ViewListCategoryLv2Query request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllCategoriesLv2();
        }
    }
}
