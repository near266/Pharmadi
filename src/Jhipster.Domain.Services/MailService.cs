using System.Threading.Tasks;
using Jhipster.Domain;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Jhipster.Domain.Services
{
    public class MailService : IMailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public MailService(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _configuration = configuration;
        }

        // private readonly SecuritySettings _securitySettings;

        // public MailService(IOptions<SecuritySettings> securitySettings)
        // {
        //     _securitySettings = securitySettings.Value;
        // }

        public virtual async Task SendPasswordResetMail(User user)
        {
            var temp = _configuration.GetValue<string>("EmailTemplate:PasswordReset");
            await _emailSender.SendEmailAsync(user.Email, "Yêu cầu mã đặt lại mật khẩu", string.Format(temp, user.Login, user.ResetKey));
            //TODO send reset Email
        }

        public virtual async Task SendActivationEmail(User user)
        {
            var temp = _configuration.GetValue<string>("EmailTemplate:ActivateAccount");
            await _emailSender.SendEmailAsync(user.Email, "Kích hoạt tài khoản", string.Format(temp, user.Login, GenLink(user.ActivationKey)));
            //TODO Activation Email
        }

        public virtual async Task SendCreationEmail(User user)
        {
            string[] parts = user.Email.Contains("@") ? user.Email.Split('@') : (user.Email + "@").Split('@');
            string result = parts[0];
            string Email = "ops@pharmadi.vn";
            var temp = _configuration.GetValue<string>("EmailTemplate:ActivateAccount");
            await _emailSender.SendEmailAsync(Email, "PharmaDI", string.Format(temp, user.Login, result,user.Email));
            //TODO Creation Email
        }   

        public virtual async Task SendPasswordForgotOTPMail(User user)
        {
            var temp = _configuration.GetValue<string>("EmailTemplate:OTPFwPass");
            await _emailSender.SendEmailAsync(user.Email, "Yêu cầu mã bảo mật", string.Format(temp, user.Login, user.ResetKey));
            //TODO send reset Email
        }

        public virtual async Task SendPasswordForgotResetMail(string newPassword, string mail)
        {
            var temp = _configuration.GetValue<string>("EmailTemplate:PasswordTemp");
            await _emailSender.SendEmailAsync(mail, "Cấp mật khẩu tạm thời (Quên mật khẩu)", string.Format(temp, "", newPassword));
            //TODO send reset Email
        }
       
        private string GenLink(string key)
        {
            var temp = _configuration.GetConnectionString("AIO");
            return $"{temp}/api/activate?key={key}";
        }

        public virtual async Task SendOrder(string MerchantName, string OrderCode, decimal TotalPayment,string Item)
        {
            string Email = "ops@pharmadi.vn";
            var temp = _configuration.GetValue<string>("EmailTemplate:MailOrder");
            await _emailSender.SendEmailAsync(Email, "Khách hàng đặt đơn", string.Format(temp, MerchantName, OrderCode, TotalPayment,Item));
        }

        public virtual async Task SendMailSp(string? Name, string? Email, string? PhoneNumber, string? NamePharma, string? Mesaage)
        {
            string mail = "ops@pharmadi.vn";
            var temp = _configuration.GetValue<string>("EmailTemplate:MailSp");
            await _emailSender.SendEmailAsync(mail, "Cần hỗ trợ", string.Format(temp, Name, NamePharma,Mesaage, PhoneNumber, Email));
        }
    }
}
