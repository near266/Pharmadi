using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Commands.CartCm
{
    public class CartDeleteCommand : IRequest<int>
    {
        public List<Guid> ids { get; set; }    
    }
    public class CartDeleteCommandHandler : IRequestHandler<CartDeleteCommand, int>
    {
        private readonly ICartRepostitory _repo;
        private readonly IMapper _mapper;
        public CartDeleteCommandHandler(ICartRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(CartDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.ids);
        }
    }

}
