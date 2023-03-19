using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.CategoryQ
{
    public class GetListCategotyQuery: IRequest<PagedList<Category>>
    {
    }
    public class GetListCategotyQueryHandler : IRequestHandler<GetListCategotyQuery, PagedList<Category>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public GetListCategotyQueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Category>> Handle(GetListCategotyQuery request, CancellationToken cancellationToken)
        {
            // var obj = _mapper.Map<Category>(request);
            return await _repo.GetListCategories();
        }
    }
}
