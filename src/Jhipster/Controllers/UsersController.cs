using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using Jhipster.Domain;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Dto;
using Jhipster.Web.Extensions;
using Jhipster.Web.Filters;
using Jhipster.Web.Rest.Utilities;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Jhipster.Dto.Authentication;
using Newtonsoft.Json;
using System;
using Module.Factor.Infrastructure.Persistences;
using Jhipster.DTO;
using System.Drawing.Printing;
using Jhipster.Service.Utilities;

namespace Jhipster.Controllers
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _log;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly FactorDbContext _factorDbContext;
        public UsersController(ILogger<UsersController> log, UserManager<User> userManager, IUserService userService,
            IMapper mapper, IMailService mailService,
            FactorDbContext factorDbContext)
        {
            _log = log;
            _userManager = userManager;
            _userService = userService;
            _mailService = mailService;
            _mapper = mapper;
            _factorDbContext = factorDbContext;
        }

        /// <summary>
        /// Tạo tài khoản (admin)
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestAlertException"></exception>
        /// <exception cref="LoginAlreadyUsedException"></exception>
        /// <exception cref="EmailAlreadyUsedException"></exception>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto userDto)
        {
            _log.LogDebug($"REST request to save User : {userDto}");
            if (!string.IsNullOrEmpty(userDto.Id))
                throw new BadRequestAlertException("A new user cannot already have an ID", "userManagement",
                    "idexists");
            // Lowercase the user login before comparing with database
            if (await _userManager.FindByNameAsync(userDto.Login.ToLowerInvariant()) != null)
                throw new LoginAlreadyUsedException();
            if (await _userManager.FindByEmailAsync(userDto.Email.ToLowerInvariant()) != null)
                throw new EmailAlreadyUsedException();

            var newUser = await _userService.CreateUser(_mapper.Map<User>(userDto), userDto.Password);
            if (!string.IsNullOrEmpty(userDto.Email))
            {
                await _mailService.SendCreationEmail(newUser);
            }

            // Qua sđt chưa xử lý
            if (string.IsNullOrEmpty(userDto.Email) && !string.IsNullOrEmpty(userDto.PhoneNumber))
            {

            }

            return CreatedAtAction(nameof(GetUser), new { login = newUser.Login }, newUser)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert("userManagement.created", newUser.Login));
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản của tài khoản admin hiện tại (admin)
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="EmailAlreadyUsedException"></exception>
        /// <exception cref="LoginAlreadyUsedException"></exception>
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            _log.LogDebug($"REST request to update User : {userDto}");
            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null && !existingUser.Id.Equals(userDto.Id)) throw new EmailAlreadyUsedException();
            existingUser = await _userManager.FindByNameAsync(userDto.Login);
            if (existingUser != null && !existingUser.Id.Equals(userDto.Id)) throw new LoginAlreadyUsedException();

            var updatedUser = await _userService.UpdateUser(_mapper.Map<User>(userDto));

            return ActionResultUtil.WrapOrNotFound(updatedUser)
                .WithHeaders(HeaderUtil.CreateAlert("userManagement.updated", userDto.Login));
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản của user theo id (admin)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            return await UpdateUser(userDto);
        }

        /// <summary>
        /// Lấy tất cả người dùng (admin) page bắt đầu từ 0
        /// </summary>
        /// <param name="pageable"></param>
        /// <returns></returns>
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromBody] ViewEmployeeDTO rq)
        {
            _log.LogDebug("REST request to get a page of Users");
            //var page = await _userManager.Users
            //    .Include(it => it.UserRoles)
            //    .ThenInclude(r => r.Role)
            //    .UsePageableAsync(pageable);
            //var userDtos = page.Content.Select(user => _mapper.Map<UserDto>(user));
            //var headers = page.GeneratePaginationHttpHeaders();
            //return Ok(userDtos).WithHeaders(headers);
            var page = _userManager.Users
                .Include(it => it.UserRoles)
                .ThenInclude(r => r.Role).AsQueryable();
            page = rq.Name != null ? page.Where(i => i.UserName.Contains(rq.Name)) : page;
            //page = rq.FromDate != null ? page.Where(i => i.CreatedDate > rq.FromDate) : page;
            //page = rq.ToDate != null ? page.Where(i => i.CreatedDate < rq.ToDate) : page;
            page = rq.PhoneNumber != null ? page.Where(i => i.PhoneNumber.Contains( rq.PhoneNumber)) : page;
            page = rq.Email != null ? page.Where(i => i.Email.Contains( rq.Email) ): page;

            var value = _mapper.Map<List<UserDto>>(page).Skip(rq.PageSize * (rq.Page - 1))
                        .Take(rq.PageSize); ;
            var key = new PagedList<UserDto>();
            key.Data = value;
            key.TotalCount = page.Count();
            return Ok(key);
        }

        /// <summary>
        /// Lấy tất cả nhóm quyền hiện tại (admin)
        /// </summary>
        /// <returns></returns>
        [HttpGet("authorities")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public ActionResult<IEnumerable<string>> GetAuthorities()
        {
            return Ok(_userService.GetAuthorities());
        }

        /// <summary>
        /// Lấy thông tin người dùng theo username (login) (admin)
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet("{login}")]
        public async Task<IActionResult> GetUser([FromRoute] string login)
        {
            _log.LogDebug($"REST request to get User : {login}");
            var result = await _userManager.Users
                .Where(user => user.Login == login)
                .Include(it => it.UserRoles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(result);
            //userDto.BranchName = _customerDbContext.Customers.Where(c => c.Login == login).Join(_customerDbContext.Branches, c => c.BranchId, b => b.Id, (c, b) => new { c, b }).Select(a => a.b.Name).FirstOrDefault();
            return ActionResultUtil.WrapOrNotFound(userDto);
        }

        /// <summary>
        /// Xóa người dùng theo username (login) (admin)
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpDelete("login")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteUser([FromBody] List<string> login)
        {
            _log.LogDebug($"REST request to delete User : {login}");
            foreach (var item in login)
            {
                await _userService.DeleteUser(item);
            }
            //return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert("userManagement.deleted", login));
            return Ok(1);
        }

        /// <summary>
        /// Đặt lại mật khẩu người dùng theo username (login) (admin)
        /// </summary>
        /// <param name="resetPasswordAdminDTO"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        [ValidateModel]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<ActionResult> ResetPasswordAdmin([FromBody] ResetPasswordAdminDTO resetPasswordAdminDTO)
        {
            _log.LogDebug($"REST request to rest Password : {JsonConvert.SerializeObject(resetPasswordAdminDTO)}");
            try
            {
                if (!resetPasswordAdminDTO.NewPassword.Equals(resetPasswordAdminDTO.RePassword)) throw new BadRequestAlertException("Password not mismatch", "RePassword", "");
                await _userService.AdminPasswordReset(resetPasswordAdminDTO.Login, resetPasswordAdminDTO.NewPassword);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [Authorize]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> SearchStaff()
        {
            _log.LogDebug($"REST request to get list staff");

            var result = await _userManager.GetUsersInRoleAsync(RolesConstants.ADMIN);
            var resultausersup = await _userManager.GetUsersInRoleAsync(RolesConstants.SUPERVIOR);
            //.Include(it => it.UserRoles)
            //.ThenInclude(r => r.Role)
            //.ToListAsync()
            ;
            var userDto = _mapper.Map<List<UserDto>>(result);
            var supDto = _mapper.Map<List<UserDto>>(resultausersup);

            foreach (var user in userDto)
            {
                user.Roles.Add(RolesConstants.ADMIN);
            }
            foreach (var user in supDto)
            {
                user.Roles.Add(RolesConstants.SUPERVIOR);
            }
            var value = userDto.Union(supDto).ToList();
            //.ThenInclude(r => r.Role)
            //.ToListAsync()
            return Ok(value);
        }
        /// <summary>
        /// thêm role cho user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UpdateRole")]
        [Authorize]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTORq rq)
        {
            _log.LogDebug($"REST request to update role");
            foreach (var item in rq.listusers)

            {
                var user = await _userManager.Users.Where(i => i.Id == item.user).FirstOrDefaultAsync();
                await _userService.UpdateRoles(user, item.roles);
            }
            return Ok(1);
        }
    }
}
