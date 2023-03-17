using AutoMapper;
using Grpc.Core;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Jhipster.Domain;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Jhipster.gRPC.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public AccountService(ILogger<AccountService> logger, IMapper mapper, IMailService mailService, UserManager<User> userManager, IUserService userService, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _mapper = mapper;
            _mailService = mailService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<RegisterResponse> RegisterAccount(RegisterRequest request, CallContext context = default)
        {
            if (!CheckPasswordLength(request.Password)) throw new InvalidPasswordException();
            var user = await _userService.RegisterUser(_mapper.Map<User>(request), request.Password);
            //await _mailService.SendActivationEmail(user);
            var res = _mapper.Map<RegisterResponse>(user);
            return res;
        }

        private static bool CheckPasswordLength(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                   password.Length >= RegisterRequest.PasswordMinLength &&
                   password.Length <= RegisterRequest.PasswordMaxLength;
        }
        public async Task<RegisterAdminResponse> RegisterAccountAdmin(RegisterAdminRequest request, CallContext context=default)
        {
            //if (!string.IsNullOrEmpty(request.Id))
            //    throw new BadRequestAlertException("A new user cannot already have an ID", "userManagement", "idexists");
            // Lowercase the user login before comparing with database
            if (await _userManager.FindByNameAsync(request.Login.ToLowerInvariant()) != null)
                throw new LoginAlreadyUsedException();
            if (await _userManager.FindByEmailAsync(request.Email.ToLowerInvariant()) != null)
                throw new EmailAlreadyUsedException();

            var newUser = await _userService.CreateUser(_mapper.Map<User>(request));
            //if (!string.IsNullOrEmpty(request.Email))
            //{
            //    await _mailService.SendCreationEmail(newUser);
            //}
          
            var res = _mapper.Map<RegisterAdminResponse>(newUser);
            return res;

        }
    }
}
