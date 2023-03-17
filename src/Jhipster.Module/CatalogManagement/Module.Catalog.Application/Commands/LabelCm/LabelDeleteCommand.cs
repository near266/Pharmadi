using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.LabelCm
{
    public class LabelDeleteCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class LabelDeleteCommandHandler : IRequestHandler<LabelDeleteCommand, int>
    {
        private readonly ILabelRepository _repo;
        private readonly IMapper _mapper;
        public LabelDeleteCommandHandler(ILabelRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(LabelDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
