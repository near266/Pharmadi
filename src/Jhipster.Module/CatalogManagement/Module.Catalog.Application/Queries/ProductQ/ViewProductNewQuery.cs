

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductNewQuery : IRequest<IEnumerable<Product>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ViewProductNewQueryHandler : IRequestHandler<ViewProductNewQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductNewQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Product>> Handle(ViewProductNewQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductNew(request.page, request.pageSize);
        }
    }

}
