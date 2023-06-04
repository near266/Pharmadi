using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Module.Catalog.Application.Commands.CategoryCm
{
    public class CategoryAddCommand: IRequest<int>
    {
        [JsonIgnore]
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string CategoryName { get; set; }
        public string? Descripton { get; set; }
        public Guid? ParentId { get; set; }
        public string? Image { get; set; }

        public bool IsLeaf { get; set; }
        [JsonIgnore]

        public Guid? CreatedBy { get; set; }
        [JsonIgnore]

        public DateTime CreatedDate { get; set; }
    }
    public class CategoryAddCommandHandler: IRequestHandler<CategoryAddCommand, int>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryAddCommandHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Category>(request);
            return await _repo.Add(obj);
        }
    }

}
