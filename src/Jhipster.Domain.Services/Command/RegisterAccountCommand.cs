using AutoMapper;
using Jhipster.Crosscutting.Constants;
using Jhipster.gRPC.Contracts.Shared.Identity;
using JHipsterNet.Core.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Domain.Services.Command
{
    public class RegisterAccountCommand:IRequest<RegisterResponse>
    {
        [DataMember(Order = 1, IsRequired = false)]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Id { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        [Required]
        [RegularExpression(Constants.LoginRegex)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Login { get; set; }

        [DataMember(Order = 3, IsRequired = false)]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Email { get; set; }

        private string? _langKey;

        [DataMember(Order = 4, IsRequired = false)]
        [MinLength(2)]
        [MaxLength(6)]
        public string LangKey
        {
            get { return _langKey; }
            set { _langKey = value; if (string.IsNullOrEmpty(_langKey)) _langKey = Constants.DefaultLangKey; }
        }

        [DataMember(Order = 5, IsRequired = false)]
        public string? CreatedBy { get; set; }

        [DataMember(Order = 6, IsRequired = false)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Order = 7, IsRequired = false)]
        [JsonProperty(PropertyName = "authorities")]
        [JsonPropertyName("authorities")]
        public ISet<string> Roles { get; set; }

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 100;

        [DataMember(Order = 8, IsRequired = true)]
        [Required]
        [MinLength(PasswordMinLength)]
        [MaxLength(PasswordMaxLength)]
        public string Password { get; set; }

        [DataMember(Order = 9, IsRequired = false)]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, RegisterResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public RegisterAccountCommandHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<RegisterResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAccount(_mapper.Map<RegisterRequest>(request));
        }
    }
}
