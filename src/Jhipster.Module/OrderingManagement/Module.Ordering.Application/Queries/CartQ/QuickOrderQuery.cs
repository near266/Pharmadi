using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Queries.CartQ
{
    public class QuickOrderQuery:IRequest<List<ViewQuickOrder>>
    {
        public Guid userId { get; set; }
        public int Type { get;set; }
        public string Keyword { get;set; }
    }
    public class QuickOrderQueryHandler : IRequestHandler<QuickOrderQuery, List<ViewQuickOrder>>
    {
        private readonly ICartRepostitory _cartRepostitory;
        public QuickOrderQueryHandler(ICartRepostitory cartRepostitory)
        {
            _cartRepostitory = cartRepostitory;
        }

        public async Task<List<ViewQuickOrder>> Handle(QuickOrderQuery request, CancellationToken cancellationToken)
        {
            return await _cartRepostitory.ViewQuick(request.userId,request.Type,request.Keyword);
        }
    }
}
