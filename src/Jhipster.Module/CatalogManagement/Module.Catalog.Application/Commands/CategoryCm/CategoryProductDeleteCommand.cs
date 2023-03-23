using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.CategoryCm
{
    public class CategoryProductDeleteCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid productId { get; set; }
        public Guid Id { get; set; }
    }
    public class CategoryProductDeleteCommandHandler : IRequestHandler<CategoryProductDeleteCommand, int>
    {
        private readonly ICategoryProductRepository _repo;
        private readonly IMapper _mapper;
        public CategoryProductDeleteCommandHandler(ICategoryProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(CategoryProductDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.productId,request.Id);
        }
    }

}
