using MediatR;
using Module.Catalog.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.BrandQ
{
    public class ImageBrandQuery : IRequest<List<string>>
    {
    }
    public class ImageBrandQueryHandler : IRequestHandler<ImageBrandQuery, List<string>>
    {
        private readonly IBrandRepository _brandRepository;
        public ImageBrandQueryHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<List<string>> Handle(ImageBrandQuery request, CancellationToken cancellationToken)
        {
            return await _brandRepository.ImageBrand();
        }
    }
}
