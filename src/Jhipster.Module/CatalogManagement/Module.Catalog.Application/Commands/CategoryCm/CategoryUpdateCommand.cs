using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.CategoryCm
{
    public class CategoryUpdateCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string CategoryName { get; set; }
        public string? Descripton { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsLeaf { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
    public class CategoryUpdateCommandHandler: IRequestHandler<CategoryUpdateCommand, int>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryUpdateCommandHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Category>(request);
            return await _repo.Update(obj);
        }
    }

}
