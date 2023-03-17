using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.BrandCm
{
    public class BrandDeleteCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class BrandDeleteCommandHandler: IRequestHandler<BrandDeleteCommand, int>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandDeleteCommandHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(BrandDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
