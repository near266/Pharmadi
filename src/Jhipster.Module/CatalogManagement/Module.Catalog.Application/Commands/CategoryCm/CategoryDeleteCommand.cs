using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.CategoryCm
{
    public class CategoryDeleteCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class CategoryDeleteCommandHandler: IRequestHandler<CategoryDeleteCommand, int>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryDeleteCommandHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
