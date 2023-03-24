

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductNewQuery : IRequest<PagedList<ProductSearchDTO>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid? userId { get; set; }
    }
    public class ViewProductNewQueryHandler : IRequestHandler<ViewProductNewQuery, PagedList<ProductSearchDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductNewQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<ProductSearchDTO>> Handle(ViewProductNewQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductNew(request.page, request.pageSize, request.userId);
        }
    }

}
