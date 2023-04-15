using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using System.Text.Json.Serialization;

namespace Module.Ordering.Application.Commands.CartCm
{
    public class CartUpdateCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        [JsonIgnore]  
        
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        public bool? IsChoice { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class CartUpdateCommandHandler : IRequestHandler<CartUpdateCommand, int>
    {
        private readonly ICartRepostitory _repo;
        private readonly IMapper _mapper;
        public CartUpdateCommandHandler(ICartRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(CartUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Cart>(request);
            return await _repo.Update(obj);
        }
    }

}
