using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Jhipster.Domain;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Dto;
using Jhipster.Dto.Authentication;
using Jhipster.Service.Utilities;
using LanguageExt.Pipes;
using LanguageExt.UnitsOfMeasure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jhipster.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _log;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserService(ILogger<UserService> log, UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, RoleManager<Role> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _log = log;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual async Task<User> CreateUser(User userToCreate,string Password)
        {
            var password = Password;
            var user = new User
            {
                Id = userToCreate.Id,
                UserName = userToCreate.Login.ToLower(),
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email.ToLower(),
                PhoneNumber = userToCreate.PhoneNumber,
                ImageUrl = userToCreate.ImageUrl,
                LangKey = userToCreate.LangKey ?? Constants.DefaultLangKey,
                PasswordHash = _userManager.PasswordHasher.HashPassword(null, password),
                ResetKey = password,
                ResetDate = DateTime.Now,
                Activated = true
            };
            await _userManager.CreateAsync(user);
            await CreateUserRoles(user, userToCreate.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
            _log.LogDebug($"Created Information for User: {user}");
            return user;
        }

        public virtual async Task<User> UpdateUser(User userToUpdate)
        {
            //TODO use Optional
            var user = await _userManager.FindByIdAsync(userToUpdate.Id);
            user.Login = userToUpdate.Login.ToLower();
            user.UserName = userToUpdate.Login.ToLower();
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Email = userToUpdate.Email;
            user.PhoneNumber = userToUpdate.PhoneNumber;
            user.ImageUrl = userToUpdate.ImageUrl;
            user.Activated = userToUpdate.Activated;
            user.LangKey = userToUpdate.LangKey;
            await _userManager.UpdateAsync(user);
            await UpdateUserRoles(user, userToUpdate.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
            return user;
        }

        public virtual async Task<User> CompletePasswordReset(string newPassword, string key)
        {
            _log.LogDebug($"Reset user password for reset key {key}");
            var user = await _userManager.Users.SingleOrDefaultAsync(it => it.ResetKey == key);
            if (user == null || user.ResetDate <= DateTime.Now.Subtract(86400.Seconds())) return null;
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            user.ResetKey = null;
            user.ResetDate = null;
            await _userManager.UpdateAsync(user);
            return user;
        }

        public virtual async Task<User> AdminPasswordReset(string login, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(login);
            if(user == null) throw new InternalServerErrorException("User could not be found");
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            user.ResetKey = null;
            user.ResetDate = null;
            await _userManager.UpdateAsync(user);
            return user;
        }

        public virtual async Task<User> RequestPasswordReset(string mail)
        {
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) return null;
            user.ResetKey = RandomUtil.GenerateResetKey();
            user.ResetDate = DateTime.Now;
            await _userManager.UpdateAsync(user);
            return user;
        }

        public virtual async Task ChangePassword(string currentClearTextPassword, string newPassword)
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var currentEncryptedPassword = user.PasswordHash;
                var isInvalidPassword =
                    _passwordHasher.VerifyHashedPassword(user, currentEncryptedPassword, currentClearTextPassword) !=
                    PasswordVerificationResult.Success;
                if (isInvalidPassword) throw new InvalidPasswordException();

                var encryptedPassword = _passwordHasher.HashPassword(user, newPassword);
                user.PasswordHash = encryptedPassword;
                await _userManager.UpdateAsync(user);
                _log.LogDebug($"Changed password for User: {user}");
            }
        }

        public virtual async Task<User> ActivateRegistration(string key)
        {
            _log.LogDebug($"Activating user for activation key {key}");
            var user = await _userManager.Users.SingleOrDefaultAsync(it => it.ActivationKey == key);
            if (user == null) return null;
            user.Activated = true;
            user.ActivationKey = null;
            await _userManager.UpdateAsync(user);

            List<UserRole> userRoles = new List<UserRole>();
            userRoles.Add(new UserRole
            {
                UserId = user.Id,
                User=user,
                
                Role = new Role
                {
                    Id= "role_merchant",
                    Name = "ROLE_MERCHANT",
                    NormalizedName = RolesConstants.USER,
                    
                },
                RoleId = RolesConstants.USER.ToLower()
            });

            await CreateUserRoles(user, userRoles.Select(iur => iur.Role.Name).ToHashSet());
            _log.LogDebug($"Activated user: {user}");
            return user;
        }


        public virtual async Task<User> RegisterUser(User userToRegister, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(userToRegister.Login.ToLower());
            if (existingUser != null)
            {
                var removed = await RemoveNonActivatedUser(existingUser);
                if (!removed) throw new LoginAlreadyUsedException();
            }

            existingUser = _userManager.Users.FirstOrDefault(it => it.Email == userToRegister.Email);
            if (existingUser != null)
            {
                var removed = await RemoveNonActivatedUser(existingUser);
                if (!removed) throw new EmailAlreadyUsedException();
            }

            existingUser = _userManager.Users.FirstOrDefault(ip => ip.PhoneNumber == userToRegister.PhoneNumber);
            if(existingUser != null)
            {
                var removed = await RemoveNonActivatedUser(existingUser);
                if (!removed) throw new PhoneNumberAlreadyUsedException();
            }

            var newUser = new User
            {
                Id = userToRegister.Id,
                Login = userToRegister.Login,
                // new user gets initially a generated password
                PasswordHash = _passwordHasher.HashPassword(null, password),
                FirstName = userToRegister.FirstName,
                LastName = userToRegister.LastName,
                Email = userToRegister.Email.ToLowerInvariant(),
                ImageUrl = userToRegister.ImageUrl,
                LangKey = userToRegister.LangKey,
                PhoneNumber = userToRegister.PhoneNumber,
                // new user is not active, but now need active
                Activated = false,
                // new user gets registration key, but now no need
                ActivationKey = RandomUtil.GenerateActivationKey()
                //ActivationKey = null,
                //TODO manage authorities
            };
            await _userManager.CreateAsync(newUser);
            //await CreateUserRoles(newUser, userToRegister.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
            _log.LogDebug($"Created Information for User: {newUser}");
            return newUser;
        }

        public virtual async Task UpdateUser(string firstName, string lastName, string email, string langKey, string imageUrl)
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.LangKey = langKey;
                user.ImageUrl = imageUrl;
                await _userManager.UpdateAsync(user);
                _log.LogDebug($"Changed Information for User: {user}");
            }
        }

        public virtual async Task<User> GetUserWithUserRoles()
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            if (userName == null) return null;
            return await GetUserWithUserRolesByName(userName);
        }

        public virtual IEnumerable<string> GetAuthorities()
        {
            return _roleManager.Roles.Select(it => it.Name).AsQueryable();
        }

        public virtual async Task DeleteUser(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user != null)
            {
                await DeleteUserRoles(user);
                await _userManager.DeleteAsync(user);
                _log.LogDebug($"Deleted User: {user}");
            }
        }

        public virtual async Task<List<ForgotPasswordMethodRsDTO>> ForgotPasswordMethod(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null) throw new InternalServerErrorException("User could not be found");
            List<ForgotPasswordMethodRsDTO> list = new List<ForgotPasswordMethodRsDTO>();
            if (user.EmailConfirmed)
            {
                list.Add(new ForgotPasswordMethodRsDTO
                {
                    Type = MethodConstants.EMAIL,
                    Value = user.Email
                });
            }

            if (user.PhoneNumberConfirmed)
            {
                list.Add(new ForgotPasswordMethodRsDTO
                {
                    Type = MethodConstants.MOB,
                    Value = user.PhoneNumber
                });
            }

            return list;
        }

        public virtual async Task<User> RequestOTPFWPass(string login, string type, string value)
        {
            User user = null;
            if (type.Equals(MethodConstants.EMAIL)){
                user = await _userManager.FindByEmailAsync(value);
                if (user == null) throw new InternalServerErrorException("Method could not be found"); ;
                user.ResetKey = RandomUtil.GenarateOTP();
                user.ResetDate = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }

            // Chưa làm
            if (type.Equals(MethodConstants.MOB)){

            }

            return user;
        }

        public virtual async Task<ForgotPasswordCompleteRpDTO> CompleteFwPass(string login, string key, string type)
        {
            _log.LogDebug($"Reset user password for reset key {key}");
            var user = await _userManager.FindByNameAsync(login);
            if (user == null || user.ResetDate <= DateTime.Now.Subtract(120.Seconds())) throw new InternalServerErrorException("OTP expire or invalid. Try again."); ;
            var newPassword = RandomUtil.GeneratePassword();
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            user.ResetKey = null;
            user.ResetDate = null;
            await _userManager.UpdateAsync(user);

            var value = string.Empty;
            if (type.Equals(MethodConstants.EMAIL)) {
                value = user.Email;
            }

            if (type.Equals(MethodConstants.MOB))
            {
                value = user.PhoneNumber;
            }
            return new ForgotPasswordCompleteRpDTO
            {
                newPassword = newPassword,
                value = value
            };
        }

        private async Task<User> GetUserWithUserRolesByName(string name)
        {
            return await _userManager.Users
                .Include(it => it.UserRoles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(it => it.UserName == name);
        }

        private async Task<bool> RemoveNonActivatedUser(User existingUser)
        {
            if (existingUser.Activated) return false;

            await _userManager.DeleteAsync(existingUser);
            return true;
        }

        private async Task CreateUserRoles(User user, IEnumerable<string> roles)
        {
            if (roles == null || !roles.Any()) return;

            foreach (var role in roles) await _userManager.AddToRoleAsync(user, role);
        }

        private async Task UpdateUserRoles(User user, IEnumerable<string> roles)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToRemove = userRoles.Except(roles).ToArray();
            var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        private async Task DeleteUserRoles(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
        }

     
    }
}
