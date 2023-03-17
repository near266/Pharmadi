using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.TagCm
{
    public class TagAddCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class TagAddCommandHandler : IRequestHandler<TagAddCommand, int>
    {
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;
        public TagAddCommandHandler(ITagRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(TagAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Tag>(request);
            return await _repo.Add(obj);
        }
    }
}
