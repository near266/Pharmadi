using Jhipster.Dto;
using Jhipster.Security.Jwt;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Web.Extensions;
using Jhipster.Web.Filters;
using Jhipster.Crosscutting.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Jhipster.Domain;
using System.Linq;
using MediatR;

namespace Jhipster.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserJwtController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;

        public UserJwtController(IAuthenticationService authenticationService, ITokenProvider tokenProvider, UserManager<User> userManager)
        {
            _authenticationService = authenticationService;
            _tokenProvider = tokenProvider;
            _userManager = userManager;
        }

        /// <summary>
        /// Đăng nhập với username/password
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [ValidateModel]
        public async Task<ActionResult<JwtToken>> Authorize([FromBody] LoginDto loginDto)
        {
            string login = string.Empty;
            //if (!string.IsNullOrEmpty(loginDto.Username) && string.IsNullOrEmpty(login))
            //{
            //    var u = await _userManager.FindByEmailAsync(loginDto.Username);
            //    if (u != null)
            //        login = u.Login;
            //}
            //if (!string.IsNullOrEmpty(loginDto.Username) && string.IsNullOrEmpty(login))
            //{
            //    var u = _userManager.Users.FirstOrDefault(i => i.PhoneNumber == loginDto.Username);
            //    if (u != null)
            //        login = u.Login;
            //}
            if (!string.IsNullOrEmpty(loginDto.Username) && string.IsNullOrEmpty(login))
            {
                login = loginDto.Username;
            }
            var user = await _authenticationService.Authenticate(login, loginDto.Password);
            var rememberMe = loginDto.RememberMe;
            var jwt = await _tokenProvider.CreateToken(user, rememberMe);
            var httpHeaders = new HeaderDictionary
            {
                [JwtConstants.AuthorizationHeader] = $"{JwtConstants.BearerPrefix} {jwt}"
            };
            return Ok(new JwtToken(jwt)).WithHeaders(httpHeaders);
        }
    }

    public class JwtToken
    {
        public JwtToken(string idToken)
        {
            IdToken = idToken;
        }

        [JsonProperty("id_token")] private string IdToken { get; }
    }
}
