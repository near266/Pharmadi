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

namespace Module.Catalog.Application.Commands.LabelCm
{
    public class LabelUpdateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string LabelName { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? Archived { get; set; }
    }
    public class LabelUpdateCommandHandler : IRequestHandler<LabelUpdateCommand, int>
    {
        private readonly ILabelRepository _repo;
        private readonly IMapper _mapper;
        public LabelUpdateCommandHandler(ILabelRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(LabelUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Label>(request);
            return await _repo.Update(obj);
        }
    }

}
