using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;


namespace Module.Catalog.Application.Queries.WarehouseProductQ
{
    public class CountProductQuery : IRequest<int>
    {
        public Guid id { get; set; }
    }
    public class CountProductQueryHandler : IRequestHandler<CountProductQuery, int>
    {
        private readonly IWarehouseProductRepository _repo;
        private readonly IMapper _mapper;
        public CountProductQueryHandler(IWarehouseProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(CountProductQuery request, CancellationToken cancellationToken)
        {
            return await _repo.CountProduct(request.id);
        }
    }

}
