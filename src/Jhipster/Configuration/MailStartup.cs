using Jhipster.Configuration;
using Jhipster.Crosscutting.Constants;
using Jhipster.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jhipster.Configuration
{
    public static class MailConfiguration
    {
        public static IServiceCollection AddMailModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Use this to load settings from appSettings file
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.Host_Address = configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.Host_Port = Convert.ToInt32(configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.Host_Username = configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.Host_Password = configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.Sender_EMail = configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
                options.Sender_Name = configuration["ExternalProviders:MailKit:SMTP:SenderName"];
                options.ClientId = configuration["ExternalProviders:MailKit:SMTP:ClientID"];
                options.ClientSecret = configuration["ExternalProviders:MailKit:SMTP:ClientSecret"];
            });
            services
                .AddAuthentication(options =>
                {
                    // This forces challenge results to be handled by Google OpenID Handler, so there's no
                    // need to add an AccountController that emits challenges for Login.
                    options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;

                    // This forces forbid results to be handled by Google OpenID Handler, which checks if
                    // extra scopes are required and does automatic incremental auth.
                    options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;

                    // Default scheme that will handle everything else.
                    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
               .AddCookie(options =>
               {
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
               });
               //.AddGoogleOpenIdConnect(options =>
               //{

               //    options.ClientId = configuration["ExternalProviders:MailKit:SMTP:ClientID"];
               //    options.ClientSecret = configuration["ExternalProviders:MailKit:SMTP:ClientSecret"];
               //});
            return services;
        }
    }
}
