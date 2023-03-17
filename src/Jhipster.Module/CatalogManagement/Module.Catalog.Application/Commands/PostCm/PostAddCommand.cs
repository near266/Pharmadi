using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.PostCm
{
    public class PostAddCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<string>? Images { get; set; }
        public List<string>? Videos { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class PostAddCommandHandler : IRequestHandler<PostAddCommand, int>
    {
        private readonly IPostContentRepository _repo;
        private readonly IMapper _mapper;
        public PostAddCommandHandler(IPostContentRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(PostAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<PostContent>(request);
            return await _repo.Add(obj);
        }
    }

}
