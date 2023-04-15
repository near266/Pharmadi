using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.TagCm
{
    public class TagProductAddCommand: IRequest<int>
    {
        [JsonIgnore]
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid TagId { get; set; }
    }
    public class TagProductAddCommandHandler: IRequestHandler<TagProductAddCommand, int>
    {
        private readonly ITagProductRepository _repo;
        private readonly IMapper _mapper;
        public TagProductAddCommandHandler(ITagProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(TagProductAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<TagProduct>(request);
            return await _repo.Add(obj);
        }
    }

}
