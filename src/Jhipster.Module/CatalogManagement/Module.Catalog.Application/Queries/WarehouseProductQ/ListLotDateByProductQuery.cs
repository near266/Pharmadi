using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.WarehouseProductQ
{
    public class ListLotDateByProductQuery : IRequest<IEnumerable<WarehouseProduct>>
    {
        public Guid id { get; set; }
    }
    public class ListLotDateByProductQueryHandler : IRequestHandler<ListLotDateByProductQuery, IEnumerable<WarehouseProduct>>
    {
        private readonly IWarehouseProductRepository _repo;
        private readonly IMapper _mapper;
        public ListLotDateByProductQueryHandler(IWarehouseProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<WarehouseProduct>> Handle(ListLotDateByProductQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ListLotDateByProduct(request.id);
        }
    }

}
