using System.Threading.Tasks;
using Jhipster.Domain;

namespace Jhipster.Domain.Services.Interfaces
{
    public interface IMailService
    {
        Task SendPasswordResetMail(User user);
        Task SendActivationEmail(User user);
        Task SendCreationEmail(User user);
        Task SendPasswordForgotOTPMail(User user);
        Task SendPasswordForgotResetMail(string newPassword, string mail);
        Task SendOrder(string MerchantName, string OrderCode, decimal TotalPayment, string Item);
        Task SendMailSp(string? Name, string? Email, string? PhoneNumber, string? NamePharma, string? Mesaage);
    }
}
