using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Web.Extensions;
using Jhipster.Web.Filters;
using Jhipster.Web.Rest.Problems;
using Jhipster.Configuration;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Jhipster.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Jhipster.Dto.Authentication;
using Newtonsoft.Json;
using Module.Factor.Application.Persistences;

namespace Jhipster.Controllers
{

    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _log;
        private readonly IMapper _userMapper;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMerchantRepository _merchantRepository;
        public AccountController(ILogger<AccountController> log,IMerchantRepository merchantRepository, UserManager<User> userManager, IUserService userService,
            IMapper userMapper, IMailService mailService)
        {
            _log = log;
            _merchantRepository = merchantRepository;
            _userMapper = userMapper;
            _userManager = userManager;
            _userService = userService;
            _mailService = mailService;
        }

        /// <summary>
        /// Đăng ký tài khoản (user)
        /// </summary>
        /// <param name="managedUserDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidPasswordException"></exception>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAccount([FromBody] ManagedUserDto managedUserDto)
        {
            if (!CheckPasswordLength(managedUserDto.Password)) throw new InvalidPasswordException();
            var user = await _userService.RegisterUser(_userMapper.Map<User>(managedUserDto), managedUserDto.Password);
            await _mailService.SendActivationEmail(user);
            return CreatedAtAction(nameof(GetAccount), user);
        }

        /// <summary>
        /// Kích hoạt tài khoản
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        [HttpGet("activate")]
        [ValidateModel]
        public async Task ActivateAccount([FromQuery(Name = "key")] string key)
        {
            var user = await _userService.ActivateRegistration(key);
            if (user == null) throw new InternalServerErrorException("Not user was found for this activation key");
            await _merchantRepository.UpdateActiveMerchant(Guid.Parse(user.Id));
        }

        /// <summary>
        /// Kiểm tra tài khoản được xác thực
        /// </summary>
        /// <returns></returns>
        [HttpGet("authenticate")]
        public string IsAuthenticated()
        {
            _log.LogDebug("REST request to check if the current user is authenticated");
            return _userManager.GetUserName(User);
        }

        /// <summary>
        /// Lấy tên nhóm quyền (Role)
        /// </summary>
        /// <returns></returns>
        [HttpGet("authorities")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public ActionResult<IEnumerable<string>> GetAuthorities()
        {
            return Ok(_userService.GetAuthorities());
        }

        /// <summary>
        /// Lấy thông tin cơ bản của người dùng (account)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        [Authorize]
        [HttpGet("account")]
        public async Task<ActionResult<UserDto>> GetAccount()
        {
            var user = await _userService.GetUserWithUserRoles();
            if (user == null) throw new InternalServerErrorException("User could not be found");
            var userDto = _userMapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        /// <summary>
        /// Cập nhật thông tin cơ bản của người dùng
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        /// <exception cref="EmailAlreadyUsedException"></exception>
        [Authorize]
        [HttpPost("account")]
        [ValidateModel]
        public async Task<ActionResult> SaveAccount([FromBody] UserDto userDto)
        {
            var userName = _userManager.GetUserName(User);
            if (userName == null) throw new InternalServerErrorException("Current user login not found");

            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null &&
                !string.Equals(existingUser.Login, userName, StringComparison.InvariantCultureIgnoreCase))
                throw new EmailAlreadyUsedException();

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new InternalServerErrorException("User could not be found");

            await _userService.UpdateUser(userDto.FirstName, userDto.LastName, userDto.Email, userDto.LangKey,
                userDto.ImageUrl);
            return Ok();
        }

        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="passwordChangeDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidPasswordException"></exception>
        [Authorize]
        [HttpPost("account/change-password")]
        [ValidateModel]
        public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeDto passwordChangeDto)
        {
            if (!CheckPasswordLength(passwordChangeDto.NewPassword)) throw new InvalidPasswordException();
            await _userService.ChangePassword(passwordChangeDto.CurrentPassword, passwordChangeDto.NewPassword);
            return Ok();
        }

        /// <summary>
        /// [Reset Password] Step 1. Gửi yêu cầu đặt lại mật khẩu (user)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EmailNotFoundException"></exception>
        [HttpPost("account/reset-password/init")]
        public async Task<ActionResult> RequestPasswordReset()
        {
            var mail = await Request.BodyAsStringAsync();
            var user = await _userService.RequestPasswordReset(mail);
            if (user == null) throw new EmailNotFoundException();
            await _mailService.SendPasswordResetMail(user);
            return Ok();
        }

        /// <summary>
        /// [Reset Password] Step 2. Hoàn thành đặt lại mật khẩu (user)
        /// </summary>
        /// <param name="keyAndPasswordDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidPasswordException"></exception>
        /// <exception cref="InternalServerErrorException"></exception>
        [HttpPost("account/reset-password/finish")]
        [ValidateModel]
        public async Task RequestPasswordReset([FromBody] KeyAndPasswordDto keyAndPasswordDto)
        {
            if (!CheckPasswordLength(keyAndPasswordDto.NewPassword)) throw new InvalidPasswordException();
            var user = await _userService.CompletePasswordReset(keyAndPasswordDto.NewPassword, keyAndPasswordDto.Key);
            if (user == null) throw new InternalServerErrorException("No user was found for this reset key");
        }

        /// <summary>
        /// [Forgot Password] Step 1. Lấy các phương thức để gửi OTP khi quên mật khẩu
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpGet("account/forgot-password-method")]
        public async Task<ActionResult<ForgotPasswordMethodRsDTO>> MethodForgotPassord(string Login)
        {
            _log.LogDebug($"REST request to get method forgot Password : {Login}");
            try
            {
                var method = await _userService.ForgotPasswordMethod(Login);
                return Ok(method);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [Forgot Password] Step 2. Yêu cầu gửi mã OTP về phương thức được gọi ở Step 1
        /// </summary>
        /// <param name="forgotPasswordOTPRqDTO"></param>
        /// <returns></returns>
        [HttpPost("account/forgot-password-otp")]
        public async Task<ActionResult> OTPForgotPassword([FromBody] ForgotPasswordOTPRqDTO forgotPasswordOTPRqDTO)
        {
            _log.LogDebug($"REST request to get otp forgot Password : {JsonConvert.SerializeObject(forgotPasswordOTPRqDTO)}");
            try
            {
                var user = await _userService.RequestOTPFWPass(forgotPasswordOTPRqDTO.Login, forgotPasswordOTPRqDTO.Type, forgotPasswordOTPRqDTO.Value);
                if(forgotPasswordOTPRqDTO.Type.Equals(MethodConstants.EMAIL))
                    await _mailService.SendPasswordForgotOTPMail(user);

                // Chưa xử lý
                if (forgotPasswordOTPRqDTO.Type.Equals(MethodConstants.MOB))
                {

                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [Forgot Password] Step 3. Kiểm tra mã OTP lấy ở Step 2 và cấp mật khẩu tạm thời gửi về phương thức lấy ở Step 1
        /// </summary>
        /// <param name="forgotPasswordResetRqDTO"></param>
        /// <returns></returns>
        [HttpPost("account/forgot-password-complete")]
        public async Task<ActionResult> CompleteForgotPassword([FromBody] ForgotPasswordResetRqDTO forgotPasswordResetRqDTO)
        {
            _log.LogDebug($"REST request to forgot password Reset : {JsonConvert.SerializeObject(forgotPasswordResetRqDTO)}");
            try
            {
                var response = await _userService.CompleteFwPass(forgotPasswordResetRqDTO.Login, forgotPasswordResetRqDTO.Key, forgotPasswordResetRqDTO.Type);
                if (forgotPasswordResetRqDTO.Type.Equals(MethodConstants.EMAIL))
                    await _mailService.SendPasswordForgotResetMail(response.newPassword, response.value);

                // Chưa xử lý
                if (forgotPasswordResetRqDTO.Type.Equals(MethodConstants.MOB))
                {

                }
                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private static bool CheckPasswordLength(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                   password.Length >= ManagedUserDto.PasswordMinLength &&
                   password.Length <= ManagedUserDto.PasswordMaxLength;
        }
    }
}
