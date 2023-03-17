using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.LabelCm
{
    public class LabelProductAddCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public Guid LabelId { get; set; }
    }
    public class LabelProductAddCommandHandler: IRequestHandler<LabelProductAddCommand, int>
    {
        private readonly ILabelProductRepository _repo;
        private readonly IMapper _mapper;
        public LabelProductAddCommandHandler(ILabelProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(LabelProductAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<LabelProduct>(request);
            return await _repo.Add(obj);
        }
    }

}
