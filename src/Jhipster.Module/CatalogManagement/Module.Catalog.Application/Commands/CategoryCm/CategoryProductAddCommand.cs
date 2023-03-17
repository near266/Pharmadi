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
    public class CategoryProductAddCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid CategoryId { get; set; }
        public bool Priority { get; set; }
    }
    public class CategoryProductAddCommandHandler: IRequestHandler<CategoryProductAddCommand, int>
    {
        private readonly ICategoryProductRepository _repo;
        private readonly IMapper _mapper;
        public CategoryProductAddCommandHandler(ICategoryProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(CategoryProductAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<CategoryProduct>(request);
            return await _repo.Add(obj);
        }
    }

}
