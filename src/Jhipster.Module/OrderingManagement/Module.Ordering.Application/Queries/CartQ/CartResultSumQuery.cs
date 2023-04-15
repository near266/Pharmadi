using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Queries.CartQ
{
    public class CartResultSumQuery : IRequest<CartResultDTO>
    {
        public Guid userId { get; set; }
    }
    public class CartResultSumQueryHandler : IRequestHandler<CartResultSumQuery, CartResultDTO>
    {
        private readonly ICartRepostitory _repo;
        private readonly IMapper _mapper;
        public CartResultSumQueryHandler(ICartRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CartResultDTO> Handle(CartResultSumQuery request, CancellationToken cancellationToken)
        {
            return await _repo.CartResultSum(request.userId);
        }
    }

}
