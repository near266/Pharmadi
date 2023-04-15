

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductForUQuery : IRequest<PagedList<ProductSearchDTO>>
    {
        public string? keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid? userId { get; set; }
    }
    public class ViewProductForUQueryHandler : IRequestHandler<ViewProductForUQuery, PagedList<ProductSearchDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductForUQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<ProductSearchDTO>> Handle(ViewProductForUQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductForU( request.keyword,request.page, request.pageSize,request.userId);
        }
    }

}
