using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.LabelQ
{
    public class LabelGetAllAdminQuery : IRequest<PagedList<Label>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }   
    }
    public class LabelGetAllAdminQueryHandler : IRequestHandler<LabelGetAllAdminQuery, PagedList<Label>>
    {
        private readonly ILabelRepository _repo;
        private readonly IMapper _mapper;
        public LabelGetAllAdminQueryHandler(ILabelRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Label>> Handle(LabelGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize);
        }
    }

}
