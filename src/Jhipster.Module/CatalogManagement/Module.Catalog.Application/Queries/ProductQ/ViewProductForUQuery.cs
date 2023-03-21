

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductForUQuery : IRequest<IEnumerable<Product>>
    {
        public string keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ViewProductForUQueryHandler : IRequestHandler<ViewProductForUQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductForUQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Product>> Handle(ViewProductForUQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductForU( request.keyword,request.page, request.pageSize);
        }
    }

}
