using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.LabelCm
{
    public class LabelProductDeleteCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid productId { get; set; }
        public Guid Id { get; set; }
    }
    public class LabelProductDeleteCommandHandler : IRequestHandler<LabelProductDeleteCommand, int>
    {
        private readonly ILabelProductRepository _repo;
        private readonly IMapper _mapper;
        public LabelProductDeleteCommandHandler(ILabelProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(LabelProductDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.productId,request.Id);
        }
    }

}
