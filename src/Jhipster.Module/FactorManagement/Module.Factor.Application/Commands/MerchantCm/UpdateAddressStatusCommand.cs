using AutoMapper;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Factor.Application.Commands.MerchantCm
{
    public class UpdateAddressStatusCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
    public class UpdateAddressStatusCommandHandler : IRequestHandler<UpdateAddressStatusCommand, int>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public UpdateAddressStatusCommandHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }
        public async Task<int> Handle(UpdateAddressStatusCommand request, CancellationToken cancellationToken)
        {
            return await _repo.UpdateAddressStatus(request.Id,request.Status);
        }
    }

}
