using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Jhipster.Domain;
using Jhipster.Dto.Authentication;

namespace Jhipster.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User userToCreate,string Password);
        IEnumerable<string> GetAuthorities();
        Task DeleteUser(string login);
        Task<User> UpdateUser(User userToUpdate);
        Task<User> CompletePasswordReset(string newPassword, string key);
        Task<User> RequestPasswordReset(string mail);
        Task ChangePassword(string currentClearTextPassword, string newPassword);
        Task<User> ActivateRegistration(string key);
        Task<User> RegisterUser(User userToRegister, string password);
        Task UpdateUser(string firstName, string lastName, string email, string langKey, string imageUrl);
        Task<User> GetUserWithUserRoles();
        Task<User> AdminPasswordReset(string login, string newPassword);
        Task<List<ForgotPasswordMethodRsDTO>> ForgotPasswordMethod(string login);
        Task<User> RequestOTPFWPass(string login, string type, string value);
        Task<ForgotPasswordCompleteRpDTO> CompleteFwPass(string login, string key, string type);
        Task deleteUserByMerchantId(Guid id);
    }
}
