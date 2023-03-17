using AutoMapper;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Factor.Application.Commands.MerchantCm
{
    public class MerchantDeleteCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class MerchantDeleteCommandHandler: IRequestHandler<MerchantDeleteCommand, int>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public MerchantDeleteCommandHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(MerchantDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
