using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductForeignQuery : IRequest<PagedList<ViewProductForeignDTO>>
    {
        public string? keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid? userId { get; set; }
    }
    public class ViewProductForeignQueryHandler : IRequestHandler<ViewProductForeignQuery, PagedList<ViewProductForeignDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductForeignQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<ViewProductForeignDTO>> Handle(ViewProductForeignQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductForeign(request.keyword, request.page, request.pageSize, request.userId);
        }
    }
}
