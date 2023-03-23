using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.LabelCm
{
    public class LabelProductUpdateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid LabelId { get; set; }
    }
    public class LabelProductUpdateCommandHandler : IRequestHandler<LabelProductUpdateCommand, int>
    {
        private readonly ILabelProductRepository _repo;
        private readonly IMapper _mapper;
        public LabelProductUpdateCommandHandler(ILabelProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(LabelProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<LabelProduct>(request);
            return await _repo.Update(obj);
        }
    }

}
