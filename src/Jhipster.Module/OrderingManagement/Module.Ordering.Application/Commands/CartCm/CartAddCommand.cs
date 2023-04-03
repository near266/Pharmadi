using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Commands.CartCm
{
    public class CartAddCommand : IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }    
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool? IsChoice { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class CartAddCommandHandler : IRequestHandler<CartAddCommand, int>
    {
        private readonly ICartRepostitory _repo;
        private readonly IMapper _mapper;
        public CartAddCommandHandler(ICartRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(CartAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Cart>(request);
            return await _repo.Add(obj);
        }
    }

}
