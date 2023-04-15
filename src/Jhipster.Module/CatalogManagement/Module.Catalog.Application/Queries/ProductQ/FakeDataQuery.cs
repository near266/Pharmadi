using MediatR;
using Module.Catalog.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class FakeDataQuery:IRequest<List<List<string>>>
    {

    }
    public class FakeDataQueryHandler : IRequestHandler<FakeDataQuery, List<List<string>>>
    {
        private readonly IProductRepository _productreposy;
        public FakeDataQueryHandler(IProductRepository productreposy)
        {
            _productreposy = productreposy;
        }
        public async Task<List<List<string>>> Handle(FakeDataQuery request, CancellationToken cancellationToken)
        {
            return await _productreposy.FakeData();
        }
    }
}
