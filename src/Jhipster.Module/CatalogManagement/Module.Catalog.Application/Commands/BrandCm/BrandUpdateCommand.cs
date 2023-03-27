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

namespace Module.Catalog.Application.Commands.BrandCm
{
    public class BrandUpdateCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string BrandName { get; set; }
        public Guid? GroupBrandId { get; set; }
        public string LogoBrand { get; set; }
        public string? Intro { get; set; }
        public bool? Pin { get; set; }
        [JsonIgnore]
        public Guid? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class BrandUpdateCommandHandler: IRequestHandler<BrandUpdateCommand, int>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandUpdateCommandHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(BrandUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Brand>(request);
            return await _repo.Update(obj);
        }
    }

}
