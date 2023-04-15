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
    public class BrandAddCommand: IRequest<int>
    {
       
        [JsonIgnore]
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string? BrandName { get; set; }
        public Guid? GroupBrandId { get; set; }
        public string LogoBrand { get; set; }
        public string Intro { get; set; }
        public bool? Pin { get; set; }
        public bool? Archived { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
    public class BrandAddCommandHandler: IRequestHandler<BrandAddCommand, int>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandAddCommandHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(BrandAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Brand>(request);
            return await _repo.Add(obj);
        }
    }

}
