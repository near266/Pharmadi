using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
//using MailKit.Net.Imap;
using MailKit.Security;
using System.Threading;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

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

    public async Task Execute(string to, string subject, string message)
    {
        try
        {
            
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(Options.Sender_EMail);
            if (!string.IsNullOrEmpty(Options.Sender_Name))
                email.Sender.Name = Options.Sender_Name;
            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
                smtp.Authenticate(Options.Host_Username, Options.Host_Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            //var clientSecrets = new ClientSecrets
            //{
            //    ClientId = Options.ClientId,
            //    ClientSecret = Options.ClientSecret
            //};

            //var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            //{
            //   // DataStore = new FileDataStore("CredentialCacheFolder", false),
            //    Scopes = new[] { "https://mail.google.com/" },
            //    ClientSecrets = clientSecrets
            //});

            //// Note: For a web app, you'll want to use AuthorizationCodeWebApp instead.
            //var codeReceiver = new LocalServerCodeReceiver();
            //var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);

            //var credential = await authCode.AuthorizeAsync(Options.Host_Username, CancellationToken.None);


            //var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);

            //using (var client = new SmtpClient())
            //{
            //    await client.ConnectAsync(Options.Host_Address, Options.Host_Port, SecureSocketOptions.SslOnConnect);
            //    await client.AuthenticateAsync(oauth2);
            //    await client.SendAsync(email,CancellationToken.None);
            //    await client.DisconnectAsync(true);
            //}

        }
        catch(Exception  ex)
        {
            throw new ArgumentException(ex.Message);
        }
  
    }
}
