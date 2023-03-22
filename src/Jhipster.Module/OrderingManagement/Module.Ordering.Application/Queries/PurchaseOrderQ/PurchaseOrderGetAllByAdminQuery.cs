using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Ordering.Application.Queries.PurchaseOrderQ
{
    public class PurchaseOrderGetAllByAdminQuery : IRequest<PagedList<PurchaseOrder>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int? status { get; set; }
        public string? codekey { get; set; }
        public string? customerkey { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
    public class PurchaseOrderGetAllByAdminQueryHandler : IRequestHandler<PurchaseOrderGetAllByAdminQuery, PagedList<PurchaseOrder>>
    {
        private readonly IPurchaseOrderRepostitory _repo;
        private readonly IMapper _mapper;
        public PurchaseOrderGetAllByAdminQueryHandler(IPurchaseOrderRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<PurchaseOrder>> Handle(PurchaseOrderGetAllByAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize,request.status, request.fromDate,request.toDate, request.codekey,request.customerkey);
        }
    }

}
