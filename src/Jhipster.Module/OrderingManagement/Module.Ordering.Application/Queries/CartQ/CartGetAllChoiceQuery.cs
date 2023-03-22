using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;



namespace Module.Ordering.Application.Queries.CartQ
{
    public class CartGetAllChoiceQuery : IRequest<List<Cart>>
    {
        public Guid userId { get; set; }
    }
    public class CartGetAllChoiceQueryHandler : IRequestHandler<CartGetAllChoiceQuery, List<Cart>>
    {
        private readonly ICartRepostitory _repo;
        private readonly IMapper _mapper;
        public CartGetAllChoiceQueryHandler(ICartRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<Cart>> Handle(CartGetAllChoiceQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetCartChoice(request.userId);
        }
    }

}
