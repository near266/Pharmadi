using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;



namespace Module.Ordering.Application.Queries.PurchaseOrderQ
{
    public class PurchaseOrderGetAllByUserQuery : IRequest<PagedList<PurchaseOrder>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int? status { get; set; }
        public Guid userId { get; set; }
    }
    public class PurchaseOrderGetAllByUserQueryHandler : IRequestHandler<PurchaseOrderGetAllByUserQuery, PagedList<PurchaseOrder>>
    {
        private readonly IPurchaseOrderRepostitory _repo;
        private readonly IMapper _mapper;
        public PurchaseOrderGetAllByUserQueryHandler(IPurchaseOrderRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<PurchaseOrder>> Handle(PurchaseOrderGetAllByUserQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllByUser(request.page, request.pageSize,request.status, request.userId);
        }
    }

}
