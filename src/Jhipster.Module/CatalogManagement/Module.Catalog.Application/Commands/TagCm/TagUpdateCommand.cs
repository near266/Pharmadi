using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Module.Catalog.Application.Commands.TagCm
{
    public class TagUpdateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string TagName { get; set; }
        [JsonIgnore]
        public Guid? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        public bool Archived { get; set; }
    }
    public class TagUpdateCommandHandler : IRequestHandler<TagUpdateCommand, int>
    {
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;
        public TagUpdateCommandHandler(ITagRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(TagUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Tag>(request);
            return await _repo.Update(obj);
        }
    }

}
