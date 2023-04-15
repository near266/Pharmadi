using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.LabelCm
{
    public class LabelAddCommand : IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string LabelName { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        public bool? Archived { get; set; }
    }
    public class LabelAddCommandHandler : IRequestHandler<LabelAddCommand, int>
    {
        private readonly ILabelRepository _repo;
        private readonly IMapper _mapper;
        public LabelAddCommandHandler(ILabelRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(LabelAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Label>(request);
            return await _repo.Add(obj);
        }
    }
}
