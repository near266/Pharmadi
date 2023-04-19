
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using Jhipster.Infrastructure.Configuration;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Infrastructure.Configuration;
public class MailKitEmailSender : IEmailSender
{
    public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options)
    {
        this.Options = options.Value;
    }

    public MailKitEmailSenderOptions Options { get; set; }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Execute(email, subject, message);
    }

    public Task Execute(string to, string subject, string message)
    {
        // create message
        /*   var email = new MimeMessage();
           email.Sender = MailboxAddress.Parse(Options.Sender_EMail);
           if (!string.IsNullOrEmpty(Options.Sender_Name))
               email.Sender.Name = Options.Sender_Name;
           email.From.Add(email.Sender);
           email.To.Add(MailboxAddress.Parse(to));
           email.Subject = subject;
           email.Body = new TextPart(TextFormat.Html) { Text = message };*/

        // send email
        //using (var smtp = new SmtpClient())
        //{
        //    smtp.Connect(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
        //    smtp.Authenticate(Options.Host_Username, Options.Host_Password);
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}
        System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
        smtpClient.UseDefaultCredentials = true;
        smtpClient.Credentials = new NetworkCredential(Options.Host_Username, Options.Host_Password);

        // Tạo đối tượng MailMessage để gửi email
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(Options.Host_Username);
        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = message;

        // Bật kết nối SSL
        mail.IsBodyHtml = true;
        smtpClient.EnableSsl = true;
        smtpClient.Host = "smtp.example.com";
        smtpClient.Port = 587;
        smtpClient.Timeout = 5000;
        mail.Priority = MailPriority.Normal;
        // Gửi email'
        smtpClient.Send(mail);

        return Task.FromResult(true);
  
    }
}
