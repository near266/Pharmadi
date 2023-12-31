﻿using AutoMapper;
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
    public class CategoryProductUpdateCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public bool Priority { get; set; }
    }
    public class CategoryProductUpdateCommandHandler: IRequestHandler<CategoryProductUpdateCommand, int>
    {
        private readonly ICategoryProductRepository _repo;
        private readonly IMapper _mapper;
        public CategoryProductUpdateCommandHandler(ICategoryProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(CategoryProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<CategoryProduct>(request);
            return await _repo.Update(obj);
        }
    }

}
