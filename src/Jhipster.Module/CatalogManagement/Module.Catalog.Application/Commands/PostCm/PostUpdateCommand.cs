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
    public class PostUpdateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<string>? Images { get; set; }
        public List<string>? Videos { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
    public class PostUpdateCommandHandler : IRequestHandler<PostUpdateCommand, int>
    {
        private readonly IPostContentRepository _repo;
        private readonly IMapper _mapper;
        public PostUpdateCommandHandler(IPostContentRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(PostUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<PostContent>(request);
            return await _repo.Update(obj);
        }
    }

}
